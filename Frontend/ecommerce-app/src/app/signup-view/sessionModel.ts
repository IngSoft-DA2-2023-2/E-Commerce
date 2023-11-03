
export interface sessionModel{
    token: string;
    user: {
        name: string;
        email: string;
        address: string;
        roles: string[];
        guid: string;
    }
}

export class sessionRequest{
    email: string;
    password: string;
    constructor(email: string, password: string){
        this.email = email;
        this.password = password;
    }
}