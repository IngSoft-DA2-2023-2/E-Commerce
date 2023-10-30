import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { product } from '../product-view/productModel';
import { userRegistrationModel } from '../signup-view/signupUserModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient:HttpClient) { }

  getProduct(){
    return this.httpClient.get<product[]>('https://localhost:7150/api/products');
  }

  postUser(data: userRegistrationModel){
    console.log("mandando la request")
    return this.httpClient.post('https://localhost:7150/api/users',data);
  }
}
