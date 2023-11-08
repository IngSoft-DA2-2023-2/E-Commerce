export class createProductModel{
    Name:string;
    Description:string;
    Price:number;
    Brand:string;
    Category:string;
    Colour:string[];
    Stock:number;


    constructor(name:string,description:string,price:number,brandName:string,categoryName:string,colours:string[],stock:number){
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Brand = brandName;
        this.Category = categoryName;
        this.Colour = colours;
        this.Stock = stock;
    }
}
