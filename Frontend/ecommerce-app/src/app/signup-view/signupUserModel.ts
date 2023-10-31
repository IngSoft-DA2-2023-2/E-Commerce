export interface userRegistrationModel {
    name: string;
    email: string;
    address: string;
    password: string;
}

export interface userModel{
    id: number;
    name: string;
    email: string;
    address: string;
    password: string;
    roles: string[];
}

export class user implements userModel{
    id: number;
    name: string;
    email: string;
    address: string;
    password: string;
    roles: string[];

    constructor(){
        this.id = 0;
        this.name = "";
        this.email = "";
        this.address = "";
        this.password = "";
        this.roles = [];
    }

}