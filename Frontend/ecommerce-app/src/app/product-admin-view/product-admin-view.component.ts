import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { UpdateProductServiceService } from '../update-product-service.service';
import { product, colour } from '../product-view/productModel';
import { createProductModel } from '../create-product-admin-view/createProductModel';

@Component({
  selector: 'app-product-admin-view',
  templateUrl: './product-admin-view.component.html',
  styleUrls: []
})
export class ProductAdminViewComponent implements OnInit {
  constructor(private api: ApiService, private router: Router, private productService: UpdateProductServiceService) { }
  feedback?: string;

  ngOnInit(): void {
    this.getProductById(undefined);
  }

  createProduct(name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement, brand: HTMLInputElement, category: HTMLInputElement, colours: HTMLInputElement,stock: HTMLInputElement) {
    const modelIn = new createProductModel(name.value, desc.value, parseInt(price.value), brand.value, category.value, colours.value.split(','),parseInt(stock.value));
    const res = this.api.postProduct(modelIn).subscribe(
      res => {
        this.feedback = "success";
      },
      err => {
        this.feedback = err.error.title;
      });

    return res;
  }

  products: product[] = [];

  getProductById(modelIn: HTMLInputElement | undefined) {
    if (!modelIn || modelIn.value == "") {
      this.api.getProduct().subscribe({
        next: res => {
          this.products = res;
          this.feedback = "";
        },
        error: err => {
          this.products = [];
          this.feedback = "An error occured";
        }
      });

      return;
    }

    const id = modelIn.value;
    const res = this.api.getProductById(id).subscribe({
      next: res => {
        this.products = [];
        if (!res) {
          this.feedback = "No results found";
          throw new Error("No results found");
        }
        this.products.push(res);
        this.feedback = "";
      },
      error: err => {
        this.products = [];
        if (err.status == 400) {
          this.feedback = "Not existing product";
        }
      }
    });
    return res;

  }

  getColourNames(colours?: colour[]): string[] {
    if (!colours) return [];
    let colourNames: string[] = [];
    colours.forEach(colour => {
      colourNames.push(colour.name);
    });
    return colourNames;
  }

  clearSearch() {
    const filteredId = document.getElementById("filteredId") as HTMLInputElement;
    if (filteredId) {
      filteredId.value = "";
    }

    this.getProductById(undefined);
  }
  goBack() {
    this.feedback = undefined;
    this.router.navigate(['']);
  }

  updateProduct(p: product) {
    this.productService.setData(p);
    this.router.navigate(['admin/updateProduct']);
  }

  openProductCreationMenu() {
    this.feedback = undefined;
    this.router.navigate(['admin/createProduct']);
  }

}
