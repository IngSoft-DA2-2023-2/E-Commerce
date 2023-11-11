import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';
import { userRegistrationInstance } from './signupUserModel';

@Component({
  selector: 'app-signup-view',
  templateUrl: './signup-view.component.html',
  styleUrls: []
})

export class SignupViewComponent {
  constructor(private api: ApiService, private router: Router) { 
    this.creatingUser = new userRegistrationInstance();
  }

  creatingUser:userRegistrationInstance;
  feedback: string = "";
  loading:boolean=false;

  signUpUser() {
    this.loading=true;
    console.log(this.creatingUser)
    this.api.postUser(this.creatingUser).subscribe({
      next: (response) => {
        this.feedback = "";
        this.api.postSession({ email: this.creatingUser.email, password: this.creatingUser.password }).subscribe({
          next: (sessionInfo) => {
            this.api.currentSession = sessionInfo;
            this.loading=false;
            this.router.navigate(['']);
          }
        })
      },
      error: (e) => {
        if(e.status==0) this.feedback = "Could not connect to the server, please try again later.";
        else if(e.status==400){
          if(!!e.error.errorMessage) this.feedback = e.error.errorMessage;
          else if(!!e.error.title) this.feedback = e.error.title;
        }
        else this.feedback = "Not valid data";
        this.loading=false;
      }
    });
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    let ret = emailRegex.test(email);
    return ret;
  }

  isLoading(): boolean {
    return this.loading;
  }

  goBack() {
    this.router.navigate(['']);
  }

}  