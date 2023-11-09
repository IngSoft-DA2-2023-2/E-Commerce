import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { product, productFilterRequestModel } from '../product-view/productModel';
import { userRegistrationModel, userRetrieveModel } from '../signup-view/signupUserModel';
import { sessionModel, sessionRequest } from '../signup-view/sessionModel';
import { createProductModel } from '../create-product-admin-view/createProductModel';
import { updateProductModel } from '../update-product-view/updateProductModel';
import { modifyUserByAdminModel } from '../update-user-by-admin-view/updateUserByAdminModel';
import { cartForPromotion, cartResponse, createCartModel, productModel, purchase, purchaseInterface } from '../purchase-view/purchaseModel';
import { UpdataSelfDataModel } from '../updata-self-data-view/updateSelfDataModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {


  constructor(private httpClient: HttpClient) { }

  currentSession: sessionModel | undefined = undefined;

  updateSession() {
    if (!this.currentSession) {
      if (localStorage.getItem('user')) {
        this.currentSession = JSON.parse(localStorage.getItem('user') as string);
      }
    }
  }

  getProduct() {
    return this.httpClient.get<product[]>('https://localhost:7150/api/products');
  }

  getBrands(){
    return this.httpClient.get<string[]>('https://localhost:7150/api/products/brands');
  }

  getCategories(){
    return this.httpClient.get<string[]>('https://localhost:7150/api/products/categories');
  }

  getColours(){
    return this.httpClient.get<string[]>('https://localhost:7150/api/products/colours');
  }

  getRoles(){
    return this.httpClient.get<string[]>('https://localhost:7150/api/users/roles');
  }

  getProductById(id: string) {
    return this.httpClient.get<product | undefined>('https://localhost:7150/api/products' + `/${id}`, { headers: { 'Authorization': `${this.currentSession?.token}` } });
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
    if(modelIn.priceRange){
      params = params.set('priceRange', modelIn.priceRange);
    }
    return this.httpClient.get<product[]>(url, { params });
  }

  postUser(data: userRegistrationModel) {
    return this.httpClient.post('https://localhost:7150/api/users', data);
  }

  postSession(data: sessionRequest) {
    return this.httpClient.post<sessionModel>('https://localhost:7150/api/sessions', data);

  }

  deleteSession() {
    let res = this.httpClient.delete('https://localhost:7150/api/sessions/', { headers: { 'Authorization': `${this.currentSession?.token}` } });
    return res;
  }

  postProduct(data: createProductModel) {
    console.log('por pegarle a la api con esto', data)
    return this.httpClient.post('https://localhost:7150/api/products', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  putProduct(id: string, data: updateProductModel) {
    const route = 'https://localhost:7150/api/products' + '/' + id;

    const requestBody = {
      name: data.Name,
      description: data.Description,
      price: data.Price,
      brand: data.Brand,
      category: data.Category,
      colour: data.Colour,
      stock: data.Stock,
      includeForPromotion: data.IncludeForPromotion
    };
    console.log(requestBody.includeForPromotion);
    return this.httpClient.put<product>(route, requestBody, {
      headers: { 'Authorization': `${this.currentSession?.token}` }
    });
  }

  getUsers() {
    return this.httpClient.get<userRetrieveModel[]>('https://localhost:7150/api/users', { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  deleteUsers(id: string) {
    return this.httpClient.delete<userRetrieveModel>(`https://localhost:7150/api/users/${id}/admin`, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  putUserByAdmin(id: string, data: modifyUserByAdminModel) {
    return this.httpClient.put<userRetrieveModel>(`https://localhost:7150/api/users/${id}/admin`, data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  postUserByAdmin(data: modifyUserByAdminModel) {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.post<userRetrieveModel>('https://localhost:7150/api/users/admin', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  putUserByThemself(data: UpdataSelfDataModel) {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.put<userRetrieveModel>('https://localhost:7150/api/users', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  postPurchase(data :purchase) { 
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.post('https://localhost:7150/api/purchases', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  getPurchaseHistory() {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.get<purchaseInterface[]>('https://localhost:7150/api/purchases', { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  postCartPrice(data :cartForPromotion){
    return this.httpClient.post<cartResponse>('https://localhost:7150/api/cart/promotions', data);
  }

}
