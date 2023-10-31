export interface sessionModel{
    token: string;
}

export class sessionRequest{
    email: string;
    password: string;
    constructor(email: string, password: string){
        this.email = email;
        this.password = password;
    }
}