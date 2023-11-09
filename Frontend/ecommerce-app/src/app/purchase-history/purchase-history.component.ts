import { Component } from '@angular/core';
import { purchaseInterface } from '../purchase-view/purchaseModel';
import { colour, product } from '../product-view/productModel';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: []
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

  colorsToString(colours: colour[]): string {
    debugger;
    console.log(colours.map(c => c.name).join(", "));
    return colours.map(c => c.name).join(", ");
  }

  convertCSharpDateTimeToJsDate(csharpDateTimeString?: Date) {
    if (!csharpDateTimeString) return "";
  
    const jsDate = new Date(csharpDateTimeString);
    const formattedDate = 
      ("0" + jsDate.getDate()).slice(-2) + "/" +
      ("0" + (jsDate.getMonth() + 1)).slice(-2) + "/" +
      jsDate.getFullYear() + " " +
      ("0" + jsDate.getHours()).slice(-2) + ":" +
      ("0" + jsDate.getMinutes()).slice(-2);
  
    return formattedDate;
  }
}
