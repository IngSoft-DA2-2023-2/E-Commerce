import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';
import { Router } from '@angular/router';
import { productFilterRequestModel } from './productModel';
import { sessionModel } from '../signup-view/sessionModel';
import { NotExpr } from '@angular/compiler';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css'],
  template: `<button (click)="openSignUpMenu()">`
})
export class ProductViewComponent implements OnInit {

  data!: product[];
  operation: string = "or";
  brands: string[] = [];
  categories: string[] = [];

  selectedBrand: string = "";
  selectedCategory: string = "";
  constructor(private api: ApiService, private router: Router) { }


  ngOnInit(): void {
    this.displayProducts();
    this.getBrands();
    this.getCategories();
  }

  getBrands(){
    this.api.getBrands().subscribe(res => {
      this.brands = res;
    });
  }

  getCategories(){
    this.api.getCategories().subscribe(res => {
      console.log(res);
      this.categories = res;
    });
  }

  displayProducts() {
    this.api.getProduct().subscribe(res => {
      this.data = res;
    });
  }

  displayFilteredProducts(name: string) {
    const filters: productFilterRequestModel = {
      name: name,
      brand: this.selectedBrand,
      category: this.selectedCategory,
      operation: this.operation
    };

    this.api.getFilteredProducts(filters).subscribe(res => {
      this.data = res;
    });
  }

  getColors(product: product): string[] {
    const colors: string[] = [];
    for (let color of product.colours) {
      colors.push(color.name);
    }
    return colors;
  }

  productCounter(product: product): number {
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    let ret:number=0;
    for (let item of cart) {
      if (item.id == product.id) {
        ret++;
      }
    }
    return ret;
  }

  addToCart(p: product){
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    let countInCart = this.productCounter(p);
    if(p.stock>countInCart){
    cart.push(p);
    localStorage.setItem('cart', JSON.stringify(cart));
  }
  else{
    console.log('No stock')
  }
}

removeFromCart(p: product){
  let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
  for(let element of cart){
    if(element.id==p.id){
      cart.splice(cart.indexOf(element),1);
      localStorage.setItem('cart', JSON.stringify(cart));
      return;
    }
  }
}
}
