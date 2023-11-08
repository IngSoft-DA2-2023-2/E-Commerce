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

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.getBrands();
    this.getCategories();
    this.getAllColours();
  }
  brands: string[] = [];
  selectedBrand: string = "";
  categories: string[] = [];
  selectedCategory: string = "";
  colors: string[] = [];
  selectedColors: string[] = [];
  feedback?: string;
  
  product: createProductModel = new createProductModel("", "", 0, this.selectedBrand, this.selectedCategory, [],0, true);

  createProduct() {
    console.log('marca',this.selectedBrand,'categoria',this.selectedCategory);
    this.feedback = '';
    this.product.Brand=this.selectedBrand;
    this.product.Category=this.selectedCategory;
    this.product.Colour=this.selectedColors;
    console.log('colores',this.product.Colour)
    const res = this.api.postProduct(this.product).subscribe({
      next: res => {this.feedback = "Success";},
      error: res => {this.feedback = "An error occured";}
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
    this.api.getBrands().subscribe(res => {
      this.brands = res;
    });
  }

  getCategories(){
    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });
  }

  getAllColours(){
    this.api.getColours().subscribe(res => {
      this.colors = res;
    });
  }

  goBack() {
    this.router.navigate(['admin/products']);
  }
  toggleInclude(){
    if(this.product.IncludeForPromotion ==true) this.product.IncludeForPromotion = false;
    else this.product.IncludeForPromotion = true;
  }
}