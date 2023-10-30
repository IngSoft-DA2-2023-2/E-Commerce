import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { product } from '../product-view/productModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient:HttpClient) { }

  getProduct(){
    return this.httpClient.get<product[]>('https://localhost:7150/api/products');
  }
}
