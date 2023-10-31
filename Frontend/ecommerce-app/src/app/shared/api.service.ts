import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { product } from '../product-view/productModel';
import { userModel, userRegistrationModel } from '../signup-view/signupUserModel';
import { sessionModel, sessionRequest } from '../signup-view/sessionModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient:HttpClient) { }
  
  currentSession: sessionModel | undefined = undefined;


  getProduct(){
    return this.httpClient.get<product[]>('https://localhost:7150/api/products');
  }

  postUser(data: userRegistrationModel){
    return this.httpClient.post('https://localhost:7150/api/users',data);
  }

  postSession(data:sessionRequest){
    return this.httpClient.post<sessionModel>('https://localhost:7150/api/sessions',data);

  }
  
}
