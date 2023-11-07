import { Component } from '@angular/core';
import { purchaseInterface } from '../purchase-view/purchaseModel';
import { product } from '../product-view/productModel';
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
    if(!this.api.currentSession) this.api.currentSession = JSON.parse(localStorage.getItem('user') || "{}");
    this.api.getPurchaseHistory().subscribe({
      next:response => {
        console.log(response);
        this.purchases = response.filter((p: purchaseInterface) => p.userId == this.api.currentSession?.user.guid as string);
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
}
