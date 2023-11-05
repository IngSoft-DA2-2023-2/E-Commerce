import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from './createProductModel';

@Component({
  selector: 'app-create-product-admin-view',
  templateUrl: './create-product-admin-view.component.html',
  styleUrls: ['./create-product-admin-view.component.css']
})
export class CreateProductAdminViewComponent {

  constructor(private api: ApiService, private router: Router) { }
  feedback?: string;
  
  product: createProductModel = new createProductModel("", "", 0, "", "", [],0);

  createProduct() {
    this.feedback = '';
    this.product.Colour=this.product.Colour.toString().split(',');
    debugger;
    const res = this.api.postProduct(this.product).subscribe({
      next: res => {this.feedback = "Success";},
      error: res => {this.feedback = "An error occured";}
    });
    return res;
  }

  goBack() {
    this.router.navigate(['admin/products']);
  }
}