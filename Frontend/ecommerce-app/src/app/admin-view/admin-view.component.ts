import { Component,OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createProductModel } from './createProductModel';
import { product , colour } from '../product-view/productModel';
import { UpdateProductServiceService } from '../update-product-service.service';
import { userRetrieveModel } from '../signup-view/signupUserModel';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.css']
})

export class AdminViewComponent implements OnInit{


  constructor(private api: ApiService, private router: Router, private dataService : UpdateProductServiceService) { }

  ngOnInit(): void {
    debugger;
      this.getUsers();
      this.getProductById(undefined);
  }

  feedback: string = "";

  token: string = this.api.currentSession?.token || "NO TOKEN";

  createProduct(name: HTMLInputElement, desc: HTMLInputElement, price: HTMLInputElement, brand: HTMLInputElement, category: HTMLInputElement, colours: HTMLInputElement) {
    const modelIn = new createProductModel(name.value, desc.value, parseInt(price.value), brand.value, category.value, colours.value.split(','));
    const res = this.api.postProduct(modelIn).subscribe(
      res => {
        this.feedback = "success";
      },
      err => {
        this.feedback = err.error.title;
      });

    return res;
  }

   products:product[]= [];

  getProductById(modelIn: HTMLInputElement | undefined) {
    if(!modelIn || modelIn.value==""){
      this.api.getProduct().subscribe(
        res => {
          this.products = res;
        },
        err => {
          this.products = [];
        }
      );
      return;
    }
  
    const id = modelIn.value;
    const res = this.api.getProductById(id).subscribe(
      res => {
        this.products=[];
        if(!res)return;
        this.products.push(res);
      },
      err => {
        this.products = [];
      }
      );
      id
      return res;

 }

 getColourNames(colours:colour[]|undefined){
  if(!colours)return;
    let colourNames:string[]=[];
    colours.forEach(colour => {
      colourNames.push(colour.name);
    });
    return colourNames;
 }

 clearSearch(){
  const filteredId = document.getElementById("filteredId") as HTMLInputElement;
  if (filteredId) {
    filteredId.value="";
  }

  this.getProductById(undefined);
 }
  goBack(){
    this.feedback="";
      this.router.navigate(['']);
  }



  updateProduct(p : product){
   this.dataService.setData(p);
    this.router.navigate(['admin/updateProduct']);
  }

  users: userRetrieveModel[] = [];
  getUsers(){
    this.api.getUsers().subscribe(
      res => {
        this.users = res;
      },
      err => {
        this.users = [];
      }

    );
    console.log(this.users);
  }

  updateUser(u:userRetrieveModel){

  }
  deleteUser(u:userRetrieveModel){
    this.api.deleteUsers(u.guid).subscribe(
      res => {
        this.getUsers();
      },
      err => {
        this.getUsers();
      }
    );
  }
}

