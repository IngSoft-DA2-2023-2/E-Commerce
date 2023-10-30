export interface product {
    id: number;
    name: string;
    price: number;
    description: string;
    brand: brand;
    category: category;
    colours: colour[];
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
