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
