import { Component, OnInit } from '@angular/core';
import { product } from '../product-view/productModel';
import { UpdateProductServiceService } from '../update-product-service.service';
import { updateProductModel } from './updateProductModel';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-update-product-view',
  templateUrl: './update-product-view.component.html',
  styleUrls: ['./update-product-view.component.css']
})

export class UpdateProductViewComponent implements OnInit {
  dataReceived?: product;

  constructor(private dataService: UpdateProductServiceService, private api: ApiService) {
    this.dataReceived = undefined;
  }

  ngOnInit(): void {
    this.dataReceived = this.dataService.getData();
    if (!this.dataReceived) {
      console.log("No data received");
      return;
    }
    const productIdElement = document.getElementById("productId") as HTMLInputElement;
    const nameElement = document.getElementById("name") as HTMLInputElement;
    const priceElement = document.getElementById("price") as HTMLInputElement;
    const descElement = document.getElementById("desc") as HTMLInputElement;
    const brandElement = document.getElementById("brand") as HTMLInputElement;
    const categoryElement = document.getElementById("category") as HTMLInputElement;
    const coloursElement = document.getElementById("colours") as HTMLInputElement;

    if (productIdElement) productIdElement.value = this.dataReceived.id.toString();
    if (nameElement) nameElement.value = this.dataReceived.name;
    if (priceElement) priceElement.value = this.dataReceived.price.toString();
    if (descElement) descElement.value = this.dataReceived.description;
    if (brandElement) brandElement.value = this.dataReceived.brand.name;
    if (categoryElement) categoryElement.value = this.dataReceived.category.name;
    if (coloursElement) coloursElement.value = this.getColours();
  
  }

  getColours(): string {
    const result: string[] = [];
    this.dataReceived?.colours.forEach(element => {
      result.push(element.name);
    });
    return result.join(",");
  }

  updateProduct(id: HTMLInputElement, name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement, brand: HTMLInputElement, category: HTMLInputElement, colours: HTMLInputElement) {
    const modelIn = new updateProductModel(
      name.value,
      parseInt(price.value),
      desc.value,
      brand.value,
      category.value,
      colours.value.split(',')
    );
    debugger;
    const res = this.api.putProduct(id.value, modelIn).subscribe(
      (response) => {
        console.log('en branch', response);
      });
    console.log(res);

    return res;
  }
}
