export class updateProductModel{
    Name:string;
    Description:string;
    Price:number;
    Brand:string;
    Category:string;
    Colours:string[];
    Stock:number;
    IncludeForPromotion:boolean;

    constructor(name:string,price:number,description:string,brandName:string,categoryName:string,colours:string[],stock:number, includeForPromotion:boolean){
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Brand = brandName;
        this.Category = categoryName;
        this.Colours = colours;
        this.Stock = stock;
        this.IncludeForPromotion = includeForPromotion;
    }
}