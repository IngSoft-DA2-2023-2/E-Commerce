import { createProductModel } from "../create-product-admin-view/createProductModel";
import { product } from "../product-view/productModel";
export class paymentMethod{
    id: number;
    categoryName: string;
    bank : string;
    flag : string;
    constructor(){
        this.id = 0;
        this.categoryName = "CreditCard";
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
export interface purchaseInterface{
    id : string;
    userId : string;
    date : Date;
    currentPromotion :string;
    total: number;
    paymentMethod: paymentMethod;
    cart : product[];
}
export interface paymentMethod{
    categoryName: string;
    bank : string;
    flag: string;
}

