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
      console.log(this.api.currentSession);
    }

// Supongamos que esta es una función en tu componente o servicio
logout() {
  if(this.api.currentSession == undefined) return;
  const token:string = this.api.currentSession?.token; // Reemplaza esto con el token real
  this.api.deleteSession(token).subscribe(
    response => {
      this.api.currentSession = undefined;
    },
  );
}

}
