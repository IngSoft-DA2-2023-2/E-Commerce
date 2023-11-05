import { Component,OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from '../create-product-admin-view/createProductModel';
import { product , colour } from '../product-view/productModel';
import { UpdateProductServiceService } from '../update-product-service.service';
import { userRetrieveModel } from '../signup-view/signupUserModel';
import { UpdateUserService } from '../update-user.service';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})

export class AdminViewComponent {

}
