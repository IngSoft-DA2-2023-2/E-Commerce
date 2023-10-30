import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';
import { userModel } from './signupUserModel';

@Component({
  selector: 'app-signup-view',
  templateUrl: './signup-view.component.html',
  styleUrls: ['./signup-view.component.css']
})

export class SignupViewComponent {
  constructor(private api:ApiService,private router:Router) { }

  goBack() {
    this.router.navigate(['']);
}

feedbackMessage:string = "";
userLogged: any = null;

signUpUser(name:HTMLInputElement,email:HTMLInputElement,address:HTMLInputElement,password:HTMLInputElement){
  console.log(`Name: ${name.value} Email: ${email.value} Address: ${address.value} Password: ${password.value}`);
  this.api.postUser({ name: name.value, email: email.value, address: address.value, password: password.value }).subscribe({
    next: (response) => {
      this.userLogged = response;
      this.feedbackMessage = "User created successfully";
      this.router.navigate(['']);
    },
    error: (e) => {
      this.feedbackMessage = e.error.errorMessage;
    }
  });
}
}  