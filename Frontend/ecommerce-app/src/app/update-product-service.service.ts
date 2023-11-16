import { Injectable } from '@angular/core';
import { product } from './product-view/productModel';

@Injectable({
  providedIn: 'root'
})
export class UpdateProductServiceService {
   modifyingProduct?: product;
  constructor() { }

  getData(){
    return this.modifyingProduct;
  }

  setData(p: product){
    this.modifyingProduct = p;
  }
}
