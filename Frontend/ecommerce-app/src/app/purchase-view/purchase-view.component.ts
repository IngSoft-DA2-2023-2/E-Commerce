import { Component } from '@angular/core';
import { product } from '../product-view/productModel';
import { ApiService } from '../shared/api.service';
import { paymentMethod } from './purchaseModel';

@Component({
  selector: 'app-purchase-view',
  templateUrl: './purchase-view.component.html',
  styleUrls: ['./purchase-view.component.css']
})
export class PurchaseViewComponent {
  constructor(private api:ApiService) { }
paymentMethod : paymentMethod = new paymentMethod();  
cart = localStorage.getItem('cart') || "[]";
cartArray = JSON.parse(this.cart);
 getColors(product: product): string[] {
  const colors: string[] = [];
  for (let color of product.colours) {
    colors.push(color.name);
  }
  return colors;
  }

  removeFromCart(p: product){
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    for(let element of cart){
      if(element.id==p.id){
        cart.splice(cart.indexOf(element),1);
        localStorage.setItem('cart', JSON.stringify(cart));
        this.cartArray = JSON.parse(localStorage.getItem('cart') || "[]");
        return;
      }
    }
  }
  hasCart(): boolean {
    console.log(this.cartArray)
    return this.cartArray.length > 0;
  }
  getPrice(): number {
    let price = 0;
    for (let product of this.cartArray) {
      price += product.price;
    }
    return price;
  }
  selectedOption(){
    return this.paymentMethod.categoryName;
  }
  onPaymentMethodChange(category: string) {
    this.paymentMethod.categoryName = category;
    if(category == "bankDebit"){
      this.paymentMethod.bank ="santander";
      this.paymentMethod.flag = "";
      return;
    }
    if(category == "credit"){
      this.paymentMethod.bank ="";
      this.paymentMethod.flag = "visa";
      return;
    }
    this.paymentMethod.bank ="";
    this.paymentMethod.flag = "";
  }
  onFlagChange(event :any) {
    this.paymentMethod.flag = event.target.value;
  }

  onBankChange(event :any) {
    this.paymentMethod.bank = event.target.value;
  }
}
