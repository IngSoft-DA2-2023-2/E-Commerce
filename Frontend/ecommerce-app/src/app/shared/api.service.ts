import { HttpClient,HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { product, productFilterRequestModel } from '../product-view/productModel';
import { userRegistrationModel } from '../signup-view/signupUserModel';
import { sessionModel, sessionRequest } from '../signup-view/sessionModel';
import { createProductModel } from '../admin-view/createProductModel';
import { updateProductModel } from '../update-product-view/updateProductModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient:HttpClient) { }
  
  currentSession: sessionModel | undefined = undefined;


  getProduct(){
    return this.httpClient.get<product[]>('https://localhost:7150/api/products');
  }

  getProductById(id:string){
    return this.httpClient.get<product|undefined>('https://localhost:7150/api/products'+`/${id}`, {headers:{'Authorization':`${this.currentSession?.token}`}});
  }

  getFilteredProducts(modelIn: productFilterRequestModel) {
    const url = 'https://localhost:7150/api/products'; 
  
    let params = new HttpParams();
  
    if (modelIn.name) {
      params = params.set('name', modelIn.name);
    }
    if (modelIn.brand) {
      params = params.set('brandName', modelIn.brand);
    }
    if (modelIn.category) {
      params = params.set('categoryName', modelIn.category);
    }
    if (modelIn.operation) {
      params = params.set('operation', modelIn.operation);
    }
    return this.httpClient.get<product[]>(url, { params });
  }

  postUser(data: userRegistrationModel){
    return this.httpClient.post('https://localhost:7150/api/users',data);
  }

  postSession(data:sessionRequest){
    return this.httpClient.post<sessionModel>('https://localhost:7150/api/sessions',data);

  }
  
  deleteSession(){
    let res = this.httpClient.delete('https://localhost:7150/api/sessions/',{headers:{'Authorization':`${this.currentSession?.token}`}});
    return res;
  }

  postProduct(data:createProductModel){
    return this.httpClient.post('https://localhost:7150/api/products',data,{headers:{'Authorization':`${this.currentSession?.token}`}});
  }


  putProduct(id:string,data:updateProductModel) {
    const route = 'https://localhost:7150/api/products'+'/'+id;
  
    let params = new HttpParams();
  
    if (data.Name) {
      params = params.set('Name', data.Name);
    }
    if (data.Price) {
      params = params.set('Price', data.Price);

    }
    if (data.Description) {
      params = params.set('Description', data.Description);
    }
    if (data.Brand) {
      params = params.set('Brand', data.Brand);
    }
    if (data.Category) {
      params = params.set('Category', data.Category);
    }
    if(data.Colour){
      params=params.set('Colour',data.Colour.toString());
    }
    debugger;
    console.log('ruta',route);
    console.log('data',data);
    
    return this.httpClient.put<product[]>(route,JSON.stringify(params),{headers:{'Authorization':`${this.currentSession?.token}`}} );
  }



}
