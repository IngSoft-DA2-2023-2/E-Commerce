import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from './createProductModel';
@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})

export class AdminViewComponent {


  constructor(private api: ApiService, private router: Router) { }

  feedback: string = "";

  createProduct(name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement, brand: HTMLInputElement, category: HTMLInputElement, colours: HTMLInputElement) {
    const modelIn = new createProductModel(name.value, desc.value, parseInt(price.value), brand.value, category.value, colours.value.split(','));
    console.log(modelIn);
    const res = this.api.postProduct(modelIn).subscribe(
      res => {
        this.feedback = "success";
      },
      err => {
        this.feedback = err.error.title;
      });

    return res;
  }
}

