import { Component, OnInit } from '@angular/core';
import { product } from '../product-view/productModel';
import { UpdateProductServiceService } from '../update-product-service.service';
import { updateProductModel } from './updateProductModel';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-product-view',
  templateUrl: './update-product-view.component.html',
  styleUrls: []
})

export class UpdateProductViewComponent implements OnInit {
  dataReceived?: product;
  feedback: string = "";

  constructor(private dataService: UpdateProductServiceService, private api: ApiService,private router: Router) {
    this.dataReceived = undefined;
  }
  selectedBrand: string = "";
  selectedCategory: string = "";
  selectedColors: string[] = [];
  brands: string[] = [];
  categories: string[] = [];
  colors: string[] = [];

  ngOnInit(): void {
    this.dataReceived = this.dataService.getData();
    this.getBrands();
    this.getCategories();
    this.getAllColours();
    this.selectedColors=this.getCurrentColours();
    if (!this.dataReceived) {
      return;
    }
    const productIdElement = document.getElementById("productId") as HTMLInputElement;
    const nameElement = document.getElementById("name") as HTMLInputElement;
    const priceElement = document.getElementById("price") as HTMLInputElement;
    const descElement = document.getElementById("desc") as HTMLInputElement;
    const stockElement = document.getElementById("stock") as HTMLInputElement;

    if (productIdElement) productIdElement.value = this.dataReceived.id.toString();
    if (nameElement) nameElement.value = this.dataReceived.name;
    if (priceElement) priceElement.value = this.dataReceived.price.toString();
    if (descElement) descElement.value = this.dataReceived.description;
    if (stockElement) stockElement.value = this.dataReceived.stock.toString();
    this.selectedBrand = this.dataReceived.brand.name;
    this.selectedCategory = this.dataReceived.category.name;

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

  getCurrentColours(): string[]{
    const result: string[] = [];
    this.dataReceived?.colours.forEach(element => {
      result.push(element.name);
    });
    console.log('colores iniciales',result)
    return result;
  }

  getAllColours(): void{
    this.api.getColours().subscribe(res => {
      this.colors = res;
    });
  }

  updateProduct(id: HTMLInputElement, name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement,stock: HTMLInputElement, includeForPromotion: HTMLInputElement) {
    console.log('colores',this.selectedColors)
    const modelIn = new updateProductModel(
      name.value,
      parseInt(price.value),
      desc.value,
      this.selectedBrand,
      this.selectedCategory,
      this.selectedColors,
      parseInt(stock.value),
      includeForPromotion.checked
    );

    const res = this.api.putProduct(id.value, modelIn).subscribe(
      (response) => {
        this.feedback = "Product updated successfully";
      },
      (error) => {
        this.feedback = "Error updating product";
      }
      );
    

    return res;
  }

  toggleColorSelection(color: string) {
    if (this.selectedColors.includes(color)) {
      this.selectedColors = this.selectedColors.filter(c => c !== color);
    } else {
      this.selectedColors.push(color);
    }
  }
  
  goBack() {
    this.router.navigate(['/admin/products']);
  }
  toggleInclude(){
    if(this.dataReceived?.includeForPromotion ==true) this.dataReceived.includeForPromotion = false;
    if(this.dataReceived?.includeForPromotion == false) this.dataReceived.includeForPromotion = true;
  }
}
