import { Component, OnInit } from '@angular/core';
import { colour, product } from '../product-view/productModel';
import { ApiService } from '../shared/api.service';
import { cartForPromotion, cartResponse, createCartModel, paymentMethod, productModel } from './purchaseModel';
import { purchase } from './purchaseModel';
@Component({
  selector: 'app-purchase-view',
  templateUrl: './purchase-view.component.html',
  styleUrls: []
})
export class PurchaseViewComponent implements OnInit {
  constructor(private api: ApiService) { }
  total = 0;
  promotion = "";

  ngOnInit(): void {
    this.getPrice().then((value) => { this.total = value; })
  }

  feedback = "";
  paymentMethod: paymentMethod = new paymentMethod();
  cart = localStorage.getItem('cart') || "[]";
  cartArray: product[] = JSON.parse(this.cart);

  getColors(product: product): string[] {
    const colors: string[] = [];
    for (let color of product.colours) {
      colors.push(color.name);
    }
    return colors;
  }

  removeFromCart(p: product): void {
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    for (let element of cart) {
      if (element.id == p.id) {
        cart.splice(cart.indexOf(element), 1);
        localStorage.setItem('cart', JSON.stringify(cart));
        this.cartArray = JSON.parse(localStorage.getItem('cart') || "[]");
        this.feedback = "Product removed from cart";
        this.getPrice().then((value) => { this.total = value; })
        return;
      }
    }
  }

  hasCart(): boolean {
    return this.cartArray.length > 0;
  }

  async getPrice(): Promise<number> {
    let price = 0;
    let sendCart: cartForPromotion = new cartForPromotion();
    for (let element of this.cartArray) {
      sendCart.cart.push(new createCartModel(element.name, element.description, element.price, 
        element.brand.name, element.category.name, 
        element.colours.map(c => c.name), element.stock, 
        element.includeForPromotion));
    }
    let response: cartResponse | undefined = undefined;
    try {
      response = await this.api.postCartPrice(sendCart).toPromise();
      price = response?.total || 0;
      this.promotion = response?.selectedPromotion || "";
    } catch (error) {
      this.feedback = "An error has occurred";
    }
    return price;
  }

  selectedOption(): string{
    return this.paymentMethod.categoryName;
  }

  onPaymentMethodChange(category: string):void {
    this.paymentMethod.categoryName = category;
    if (category == "BankDebit") {
      this.paymentMethod.bank = "santander";
      this.paymentMethod.flag = "";
      return;
    }
    if (category == "CreditCard") {
      this.paymentMethod.bank = "";
      this.paymentMethod.flag = "visa";
      return;
    }
    this.paymentMethod.bank = "";
    this.paymentMethod.flag = "";
  }

  onFlagChange(event: Event): void {
    debugger;
    console.log('flagg', event)
    const flag = (event.target as HTMLSelectElement).value;
    this.paymentMethod.flag = flag;
  }

  onBankChange(event: Event): void {
    debugger;
    const selectedBank = (event.target as HTMLSelectElement).value;
    this.paymentMethod.bank = selectedBank;
  }

  transferColors(colors: colour[]): string[] {
    let ret = [];
    for (let color of colors) {
      ret.push(color.name);
    }
    return ret;
  }

  purchase(): void {
    this.feedback = "Processing...";
    if (!this.api.currentSession) this.api.currentSession = JSON.parse(localStorage.getItem('session') || "");
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    let p = new purchase();
    p.PaymentMethod = this.paymentMethod;
    let returnCart = [];
    for (let element of cart) {
      let colorName = this.transferColors(element.colours);
      let returnProduct = new productModel(element.id, element.name, element.description, element.price, element.brand.name, element.category.name, colorName, element.stock);
      returnCart.push(returnProduct);
    }
    p.Cart = returnCart;
    this.api.postPurchase(p).subscribe({
      next: res => {
        localStorage.setItem('cart', JSON.stringify([]));
        this.cartArray = JSON.parse(localStorage.getItem('cart') || "[]");
        this.feedback = "Successfully purchased";
      }, error: error => {
        if (error.status == 0) this.feedback = "Could not connect to server";
        else this.feedback = "An error has occurred";
      }
    });
  }

  isBuyer(): boolean {
    return this.api.currentSession?.user.roles.includes("buyer") || false;
  }
}
