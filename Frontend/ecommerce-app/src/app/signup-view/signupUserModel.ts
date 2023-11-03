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
}

export interface userRetrieveModel{
    guid: number;
    name: string;
    email: string;
    address: string;
    roles: string[];
}