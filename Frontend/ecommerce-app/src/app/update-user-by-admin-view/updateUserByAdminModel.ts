export class modifyUserByAdminModel {
    name: string;
    password?: string;
    address: string;
    roles: string[];
    constructor(name: string, address: string, roles: string[]) {
        this.name = name;
        this.address = address;
        this.roles = roles;
    }

}