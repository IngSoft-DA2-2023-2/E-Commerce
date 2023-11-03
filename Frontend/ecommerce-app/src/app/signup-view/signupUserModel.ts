export interface userRegistrationModel {
    name: string;
    email: string;
    address: string;
    password: string;
}

export interface userModel{
    id: string;
    name: string;
    email: string;
    address: string;
    password: string;
}

export interface userRetrieveModel{
    guid: string;
    name: string;
    email: string;
    address: string;
    roles: string[];
}