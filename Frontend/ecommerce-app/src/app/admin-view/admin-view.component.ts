import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from './createProductModel';
import { product , colour } from '../product-view/productModel';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})

export class AdminViewComponent {


  constructor(private api: ApiService, private router: Router) { }

  feedback: string = "";

  token: string = this.api.currentSession?.token || "NO TOKEN";

  createProduct(name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement, brand: HTMLInputElement, category: HTMLInputElement, colours: HTMLInputElement) {
    const modelIn = new createProductModel(name.value, desc.value, parseInt(price.value), brand.value, category.value, colours.value.split(','));
    const res = this.api.postProduct(modelIn).subscribe(
      res => {
        this.feedback = "success";
      },
      err => {
        this.feedback = err.error.title;
      });

    return res;
  }

   products:product[]= [];

  getProductById(modelIn: HTMLInputElement) {
    if(modelIn.value==""){
      this.api.getProduct().subscribe(
        res => {
          this.products = res;
        },
        err => {
          this.products = [];
        }
      );
      return;
    }
  
    const id = modelIn.value;
    const res = this.api.getProductById(id).subscribe(
      res => {
        this.products=[];
        if(!res)return;
        this.products.push(res);
      });
      id
      return res;

 }

 getColourNames(colours:colour[]|undefined){
  if(!colours)return;
    let colourNames:string[]=[];
    colours.forEach(colour => {
      colourNames.push(colour.name);
    });
    return colourNames;
 }


  goBack(){
    this.feedback="";
      this.router.navigate(['']);
  }
}

