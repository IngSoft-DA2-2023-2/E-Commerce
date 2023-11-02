import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';
import { Router } from '@angular/router';
import { productFilterRequestModel } from './productModel';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css'],
  template: `<button (click)="openSignUpMenu()">`
})
export class ProductViewComponent implements OnInit {

  data!: product[];
  operation: string = "or";



//mientras
session: any = undefined;	

  constructor(private api: ApiService, private router: Router) { }


  ngOnInit(): void {
    this.displayProducts();
    this.session = this.api.currentSession?.token;
  }
  displayProducts() {
    this.api.getProduct().subscribe(res => {
      this.data = res;
    });
  }

  displayFilteredProducts(name: string, brand: string, category: string) {
    const filters: productFilterRequestModel = {
      name: name,
      brand: brand,
      category: category,
      operation: this.operation

    };

    this.api.getFilteredProducts(filters).subscribe(res => {
      this.data = res;
    });
  }

  openSignUpMenu() {
    this.router.navigate(['/signup']);
  }
  openSignInMenu() {
    this.router.navigate(['/signin']);
  }
  seeLoggedInfo() {
    console.log(this.api.currentSession);
  }

  logout() {
    if (this.api.currentSession == undefined) return;
    const token: string = this.api.currentSession?.token;
    this.api.deleteSession().subscribe(
      response => {
        this.api.currentSession = undefined;
      },
    );
  }

  displayAdminMenu(){
    console.log("desplegando...")
    this.router.navigate(['/admin']);
  }
}
