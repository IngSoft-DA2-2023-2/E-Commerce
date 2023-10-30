import { Component,OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { product } from './productModel';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css']
})
export class ProductViewComponent implements OnInit {
    
    data!:product[];
    constructor(private api:ApiService) { }
  
    ngOnInit(): void {
      this.displayProducts();

    }
    displayProducts(){
      this.api.getProduct().subscribe(res=>{
       this.data = res;
       console.log(res);
      });
    }

}
