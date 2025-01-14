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
    includeForPromotion:boolean;
    
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
    Colours:string[];
    Stock:number;
    includeForPromotion:boolean;


    constructor(id:string, name:string,description:string,price:number,brandName:string,categoryName:string,colours:string[],stock:number, includeForPromotion:boolean){
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Brand = brandName;
        this.Category = categoryName;
        this.Colours = colours;
        this.Stock = stock;
        this.includeForPromotion = includeForPromotion;
    }
}
export class cartResponse{
    selectedPromotion : string;
    total : number;
    cart : createCartModel[];
    constructor(){
        this.selectedPromotion = "";
        this.total = 0;
        this.cart = [];
    }
}
export class createCartModel{
    name:string;
    description:string;
    price:number;
    brand:string;
    category:string;
    colours:string[];
    stock:number;
    includeForPromotion:boolean;

    constructor(name:string,description:string,price:number,brandName:string,categoryName:string,colours:string[],stock:number, includeForPromotion:boolean){
        this.name = name;
        this.description = description;
        this.price = price;
        this.brand = brandName;
        this.category = categoryName;
        this.colours = colours;
        this.stock = stock;
        this.includeForPromotion = includeForPromotion;

    }
}
export class cartForPromotion{
    cart : createCartModel[];
    constructor(){
        this.cart = [] as createCartModel[];
    }
}

