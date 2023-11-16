import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';
import { Router } from '@angular/router';
import { productFilterRequestModel } from './productModel';

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
  filterByPrice: boolean = false;
  priceFrom?: number;
  priceTo?: number;
  alertMessage: string = "";
  selectedBrand: string = "";
  selectedCategory: string = "";

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.displayProducts();
    this.getBrands();
    this.getCategories();
  }

  getBrands(): void {
    this.api.getBrands().subscribe(res => {
      this.brands = res;
    });
  }

  getCategories(): void {
    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });
  }

  displayProducts(): void {
    this.alertMessage = "Loading...";
    this.api.getProduct().subscribe({
      next: res => {
        this.data = res;
        this.alertMessage = "";
      },
      error: err => {
        if (err.status == 0) {
          this.alertMessage = "Could not connect to the server, please try again later.";
        } else {
          this.alertMessage = "An error has occured, please try again later.";
        }
      }
    });
  }

  displayFilteredProducts(name: string): void {
    const filters: productFilterRequestModel = {
      name: name,
      brand: this.selectedBrand,
      category: this.selectedCategory,
      operation: this.operation,
      priceRange: undefined
    };
    if (this.filterByPrice) { filters.priceRange = "" + this.priceFrom + "-" + this.priceTo; }
    this.alertMessage = "Loading...";
    this.api.getFilteredProducts(filters).subscribe({
      next: res => {
        this.data = res;
        this.alertMessage = "";
        if (res.length == 0) this.alertMessage = "No products found.";
      },
      error: err => {
        if (err.status == 0) {
          this.alertMessage = "Could not connect to the server, please try again later.";
        } else {
          this.alertMessage = "An error has occured, please try again later.";
        }
      }
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
    const cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    let ret: number = 0;
    for (let item of cart) {
      if (item.id == product.id) {
        ret++;
      }
    }
    return ret;
  }

  addToCart(p: product): void {
    const cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    const countInCart = this.productCounter(p);
    if (p.stock > countInCart) {
      cart.push(p);
      localStorage.setItem('cart', JSON.stringify(cart));
    }
  }

  removeFromCart(p: product): void {
    let cart = JSON.parse(localStorage.getItem('cart') || "[]") as product[];
    for (let element of cart) {
      if (element.id == p.id) {
        cart.splice(cart.indexOf(element), 1);
        localStorage.setItem('cart', JSON.stringify(cart));
        return;
      }
    }
  }
}
