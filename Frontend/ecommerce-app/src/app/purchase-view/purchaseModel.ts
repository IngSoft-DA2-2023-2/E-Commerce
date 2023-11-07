import { createProductModel } from "../create-product-admin-view/createProductModel";
export class paymentMethod{
    id: number;
    categoryName: string;
    bank : string;
    flag : string;
    constructor(){
        this.id = 0;
        this.categoryName = "credit";
        this.bank = "";
        this.flag = "visa";
    }
}
export class purchase{
    PaymentMethod: paymentMethod;
    Cart: createProductModel[];
    constructor(){
        this.PaymentMethod = new paymentMethod();
        this.Cart = [];
    }
}
