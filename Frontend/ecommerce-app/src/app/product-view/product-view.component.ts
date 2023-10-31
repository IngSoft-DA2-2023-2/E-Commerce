import { Component,OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css'],
  template: `<button (click)="openSignUpMenu()">`
})
export class ProductViewComponent implements OnInit {
    
    data!:product[];
    constructor(private api:ApiService,private router:Router) { }
  
    ngOnInit(): void {
      this.displayProducts();

    }
    displayProducts(){
      this.api.getProduct().subscribe(res=>{
       this.data = res;
      });
    }

    openSignUpMenu() {
      this.router.navigate(['/signup']);
    }
    openSignInMenu() {
      this.router.navigate(['/signin']);
    }

    seeLoggedInfo(){
      console.log(this.api.loggedUser);
    }
}
