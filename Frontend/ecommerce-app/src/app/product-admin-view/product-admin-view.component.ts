import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { UpdateProductServiceService } from '../update-product-service.service';
import { product, colour } from '../product-view/productModel';

@Component({
  selector: 'app-product-admin-view',
  templateUrl: './product-admin-view.component.html',
  styleUrls: []
})
export class ProductAdminViewComponent implements OnInit {
  constructor(private api: ApiService, private router: Router, private productService: UpdateProductServiceService) {
    if(!this.api.currentSession?.user.roles.includes('admin')) this.router.navigate(['']);
   }

  feedback?: string;
  loading: boolean = false;
  products: product[] = [];

  ngOnInit(): void {
    this.feedback = ""
    this.getProducts(undefined);
  }

  getProducts(modelIn: HTMLInputElement | undefined): void{
    this.loading = true;
    if (!modelIn || modelIn.value == "") {
      this.getAllProducts();
    } else {
      this.getProductById(modelIn);
    }
  }

  getProductById(modelIn: HTMLInputElement): void {
    this.loading = true;
    const id = modelIn.value;
    this.api.getProductById(id).subscribe({
      next: res => {
        if (!res) {
          this.products = [];
          this.feedback = "No results found";
        } else {
          this.products = [res];
          this.feedback = "";
        }
        this.loading = false;
      },
      error: error => {
        if (error.status === 0) {
          this.feedback = "Could not connect to the server, please try again later.";
        } else if (error.status === 400) {
          this.feedback = "Not existing product";
          this.products = [];
        } else {
          this.feedback = "An error has occurred, please try again later.";
          this.products = [];
        }
        this.loading = false;
      }
  });
  }

  getAllProducts() : void{
    let that = this;
    this.api.getProduct().subscribe({
      next: res => {
        that.products = res;
        that.feedback = "";
        that.loading = false;
        return;
      },
      error: err => {
        that.products = [];
        if(err.status == 0) that.feedback = "Could not connect to the server, please try again later.";
        else that.feedback = "An error occured";
        that.loading = false;
        return;
      }
    });
    return;
  }

  getColourNames(colours?: colour[]): string[] {
    if (!colours) return [];
    let colourNames: string[] = [];
    colours.forEach(colour => {
      colourNames.push(colour.name);
    });
    return colourNames;
  }

  clearSearch(): void {
    const filteredId = document.getElementById("filteredId") as HTMLInputElement;
    if (filteredId) {
      filteredId.value = "";
    }
    this.getProducts(undefined);
  }

  isLoading(): boolean {
    return this.loading;
  }

  goBack(): void{
    this.feedback = undefined;
    this.router.navigate(['']);
  }

  updateProduct(p: product): void {
    this.productService.setData(p);
    this.router.navigate(['admin/updateProduct']);
  }

  openProductCreationMenu(): void {
    this.feedback = undefined;
    this.router.navigate(['admin/createProduct']);
  }
}
