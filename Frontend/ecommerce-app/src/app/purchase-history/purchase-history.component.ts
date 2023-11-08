import { Component } from '@angular/core';
import { purchaseInterface } from '../purchase-view/purchaseModel';
import { colour, product } from '../product-view/productModel';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.css']
})
export class PurchaseHistoryComponent {
  constructor(private api:ApiService) { }
  purchases : purchaseInterface[] = [];
  ngOnInit(): void {
    this.api.getPurchaseHistory().subscribe({
      next:response => {
        console.log(response);
        this.purchases = response;
        console.log(this.purchases);
      },
      error:error => {
        console.log(error);
      }
  });
  }

  hasPurchases(): boolean {
    return this.purchases.length > 0;
  }

  getProductNames(product?: product[]): string {
    let ret = [];
    if(!product) return "";
    for(let elem of product){
    ret.push(elem.name);
    }
    return ret.join(", ");
  }
  colorsToString(colours: colour[]): string {
    debugger;
    console.log(colours.map(c => c.name).join(", "));
    return colours.map(c => c.name).join(", ");
  }
}
