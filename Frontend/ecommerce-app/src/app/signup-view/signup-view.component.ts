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



signUpUser(name:HTMLInputElement,email:HTMLInputElement,address:HTMLInputElement,password:HTMLInputElement){
  this.api.postUser({ name: name.value, email: email.value, address: address.value, password: password.value }).subscribe({
    next: (response) => {
      this.feedbackMessage = "";
      this.api.postSession({ email: email.value, password: password.value }).subscribe({
      next: (sessionInfo) =>{
                              this.api.currentSession = sessionInfo;
                              this.router.navigate(['']);
      }})
      
      
      

    },
    error: (e) => {
      this.feedbackMessage = e.error.errorMessage;
    }
  });
}
}  