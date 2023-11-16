export class createProductModel{
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
