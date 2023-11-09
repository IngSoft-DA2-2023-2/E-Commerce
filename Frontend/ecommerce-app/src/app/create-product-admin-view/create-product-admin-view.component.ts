import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from './createProductModel';

@Component({
  selector: 'app-create-product-admin-view',
  templateUrl: './create-product-admin-view.component.html',
  styleUrls: ['./create-product-admin-view.component.css']
})
export class CreateProductAdminViewComponent implements OnInit {

  constructor(private api: ApiService, private router: Router) {
    this.product= new createProductModel("", "", 0, this.selectedBrand, this.selectedCategory, [],0, true);
   }

  ngOnInit(): void {
    this.getBrands();
    this.getCategories();
    this.getAllColours();
  }

  ngModelOptions = { standalone: true };


  brands: string[] = [];
  selectedBrand: string = "";
  categories: string[] = [];
  selectedCategory: string = "";
  colors: string[] = [];
  selectedColors: string[] = [];
  feedback?: string;
  product: createProductModel;
  loading: boolean = false;

  createProduct() {
    console.log('creando prod',this.product);
    this.loading=true;
    this.product.brand=this.selectedBrand;
    this.product.category=this.selectedCategory;
    this.product.colours=this.selectedColors;
    console.log('colores',this.product.colours)
    const res = this.api.postProduct(this.product).subscribe({
      next: res => {this.feedback = "Success"; this.loading=false;},
      error: res => {
        console.log(res)
        if(res.status==0) this.feedback = "Could not connect to server";
        else if(res.status==400) this.feedback = res.error.errorMessage;
        else this.feedback = "An error occurred";

        this.loading=false;
      }
    });
    return res;
  }

  toggleColorSelection(color: string) {
    if (this.selectedColors.includes(color)) {
      this.selectedColors = this.selectedColors.filter(c => c !== color);
    } else {
      this.selectedColors.push(color);
    }
  }
  getBrands(){
    this.loading=true;
    this.api.getBrands().subscribe(res => {
      this.brands = res;
    });
    this.loading=false;
  }

  getCategories(){
    this.loading=true;
    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });
    this.loading=false;
  }

  getAllColours(){
    this.loading=true;
    this.api.getColours().subscribe(res => {
      this.colors = res;
    });
    this.loading=false;
  }

  isLoading(){
    return this.loading;
  }

  goBack() {
    this.router.navigate(['admin/products']);
  }
  toggleInclude(){
    if(this.product.includeForPromotion) this.product.includeForPromotion = false;
    else this.product.includeForPromotion = true;
  }
}