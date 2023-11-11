import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  url: string = 'https://localhost:7150/api';

  currentSession: sessionModel | undefined = undefined;

  updateSession() {
    if (!this.currentSession) {
      if (localStorage.getItem('user')) {
        this.currentSession = JSON.parse(localStorage.getItem('user') as string);
      }
    }
  }

  getProduct() {
    return this.httpClient.get<product[]>(this.url + '/products');
  }

  getBrands() {
    return this.httpClient.get<string[]>(this.url + '/products/brands');
  }

  getCategories() {
    return this.httpClient.get<string[]>(this.url + '/products/categories');
  }

  getColours() {
    return this.httpClient.get<string[]>(this.url + '/products/colours');
  }

  getRoles() {
    return this.httpClient.get<string[]>(this.url + '/users/roles');
  }

  getProductById(id: string) {
    return this.httpClient.get<product | undefined>(this.url + '/products' + `/${id}`, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  getFilteredProducts(modelIn: productFilterRequestModel) {
    const url = this.url + '/products';
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
    if (modelIn.priceRange) {
      params = params.set('priceRange', modelIn.priceRange);
    }
    return this.httpClient.get<product[]>(url, { params });
  }

  postUser(data: userRegistrationModel) {
    return this.httpClient.post<userRetrieveModel>(this.url + '/users', data);
  }

  postSession(data: sessionRequest) {
    return this.httpClient.post<sessionModel>(this.url + '/sessions', data);

  }

  deleteSession() {
    let res = this.httpClient.delete<userRetrieveModel>(this.url + '/sessions/', { headers: { 'Authorization': `${this.currentSession?.token}` } });
    return res;
  }

  postProduct(data: createProductModel) {
    console.log('por pegarle a la api con esto', data)
    return this.httpClient.post<product>(this.url + '/products', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  putProduct(id: string, data: updateProductModel) {
    const route = this.url + '/products' + '/' + id;
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
    return this.httpClient.get<userRetrieveModel[]>(this.url + '/users', { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  deleteUsers(id: string) {
    return this.httpClient.delete<userRetrieveModel>(this.url + `/users/${id}/admin`, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  putUserByAdmin(id: string, data: modifyUserByAdminModel) {
    return this.httpClient.put<userRetrieveModel>(this.url + `/users/${id}/admin`, data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  postUserByAdmin(data: modifyUserByAdminModel) {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.post<userRetrieveModel>(this.url + '/users/admin', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  putUserByThemself(data: UpdataSelfDataModel) {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.put<userRetrieveModel>(this.url + '/users', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }

  postPurchase(data: purchase) {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.post<purchaseInterface>(this.url + '/purchases', data, { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  getPurchaseHistory() {
    if (!this.currentSession) {
      throw new Error('no session');
    }
    return this.httpClient.get<purchaseInterface[]>(this.url + '/purchases', { headers: { 'Authorization': `${this.currentSession?.token}` } });
  }
  postCartPrice(data: cartForPromotion) {
    return this.httpClient.post<cartResponse>(this.url + '/cart/promotions', data);
  }

}
