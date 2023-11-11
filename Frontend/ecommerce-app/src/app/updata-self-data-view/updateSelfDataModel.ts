export class UpdataSelfDataModel{
    name: string;	
    password: string;
    address: string;

    constructor(name: string, password: string, address: string){
        this.name = name;
        this.password = password;
        this.address = address;
    }
}