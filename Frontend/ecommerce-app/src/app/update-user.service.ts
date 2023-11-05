import { Injectable } from '@angular/core';
import { userRetrieveModel } from './signup-view/signupUserModel';


@Injectable({
  providedIn: 'root'
})
export class UpdateUserService {
  modifyingUser?: userRetrieveModel;
  constructor() { }

  getData():userRetrieveModel | undefined{
    return this.modifyingUser;
  }

  setData(u: userRetrieveModel){
    this.modifyingUser = u;
  }
}
