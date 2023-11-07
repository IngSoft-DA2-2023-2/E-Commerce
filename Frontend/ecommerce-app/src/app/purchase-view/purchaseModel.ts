import { createProductModel } from "../create-product-admin-view/createProductModel";
import { product } from "../product-view/productModel";
export class paymentMethod{
    id: string;
    categoryName: string;
    bank : string;
    flag : string;
    constructor(){
        this.id = "";
        this.categoryName = "CreditCard";
        this.bank = "";
        this.flag = "visa";
    }
}
export class purchase{
    PaymentMethod: paymentMethod;
    Cart: productModel[];
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

export class productModel{
    Id: string;
    Name:string;
    Description:string;
    Price:number;
    Brand:string;
    Category:string;
    Colour:string[];
    Stock:number;


    constructor(id:string, name:string,description:string,price:number,brandName:string,categoryName:string,colours:string[],stock:number){
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Brand = brandName;
        this.Category = categoryName;
        this.Colour = colours;
        this.Stock = stock;
    }
}

