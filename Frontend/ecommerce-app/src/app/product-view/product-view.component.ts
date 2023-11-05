import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';
import { Router } from '@angular/router';
import { productFilterRequestModel } from './productModel';
import { sessionModel } from '../signup-view/sessionModel';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css'],
  template: `<button (click)="openSignUpMenu()">`
})
export class ProductViewComponent implements OnInit {

  data!: product[];
  operation: string = "or";



  constructor(private api: ApiService, private router: Router) { }


  ngOnInit(): void {
    this.displayProducts();
  }

  displayProducts() {
    this.api.getProduct().subscribe(res => {
      this.data = res;
    });
  }

  displayFilteredProducts(name: string, brand: string, category: string) {
    const filters: productFilterRequestModel = {
      name: name,
      brand: brand,
      category: category,
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
}
