import { Component } from '@angular/core';
import { product } from '../product-view/productModel';

@Component({
  selector: 'app-purchase-view',
  templateUrl: './purchase-view.component.html',
  styleUrls: ['./purchase-view.component.css']
})
export class PurchaseViewComponent {
 cart = localStorage.getItem('cart') || "[]";
cartArray = JSON.parse(this.cart);

 getColors(product: product): string[] {
  const colors: string[] = [];
  for (let color of product.colours) {
    colors.push(color.name);
  }
  return colors;
  }
  removeProductFromCart(product: product) {
    let cart = localStorage.getItem('cart') || "[]";
    let cartArray = JSON.parse(cart);
    cartArray = cartArray.filter((item: product) => item.id !== product.id);
    localStorage.setItem('cart', JSON.stringify(cartArray));
    this.cartArray = cartArray;
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
}
