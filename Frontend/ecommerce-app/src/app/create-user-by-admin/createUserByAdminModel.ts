export class createUserByAdminModel {
    name: string;
    email: string;
    password?: string;
    address: string;
    roles: string[];

    constructor(name: string,email:string, address: string, roles: string[]) {
        this.name = name;
        this.email=email;
        this.address = address;
        this.roles = roles;
    }

}