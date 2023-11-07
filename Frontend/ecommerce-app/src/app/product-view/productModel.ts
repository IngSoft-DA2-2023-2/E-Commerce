export interface product {
    id: number;
    name: string;
    price: number;
    description: string;
    brand: brand;
    category: category;
    colours: colour[];
    stock: number;
}

export interface brand{
    id: number;
    name: string;
}

export interface colour{
    id: number;
    name: string;
}

export interface category{
    id: number;
    name: string;
}

export class productFilterRequestModel{
    name:string | undefined = undefined;
    brand:string | undefined = undefined;
    category:string | undefined = undefined;
    priceRange:string | undefined = undefined;
    operation:string | undefined= undefined;

    constructor(name:string | undefined,brand:string | undefined,category:string | undefined,operation:string | undefined){
        this.name = name;
        this.brand = brand;
        this.category = category;
        this.operation = operation;
    }
}