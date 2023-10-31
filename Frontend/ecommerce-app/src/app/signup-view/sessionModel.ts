import { userModel } from '../signup-view/signupUserModel';


export interface sessionModel{
    token: string;
    user: userModel;
}

export class sessionRequest{
    email: string;
    password: string;

    constructor(email: string, password: string){
        this.email = email;
        this.password = password;
    }
}