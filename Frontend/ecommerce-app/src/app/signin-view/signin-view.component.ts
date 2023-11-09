import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { sessionRequest } from '../signup-view/sessionModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin-view',
  templateUrl: './signin-view.component.html',
  styleUrls: ['./signin-view.component.css']
})
export class SigninViewComponent {

  constructor(private api: ApiService, private router: Router) { }

  feedback: string = "";
  signInUser(email: string, password: string) {
    setTimeout(() => {
      this.feedback = "Loading...";
      if (!this.isValidEmail(email)) {
        this.feedback = "Invalid email";
        return;
      }
      
      this.api.postSession(new sessionRequest(email, password)).subscribe({
        next: res => {
          this.api.currentSession = res;
          this.feedback = "success";
          localStorage.setItem('user', JSON.stringify(res));
          this.router.navigate(['']);
        },
        error: err => {
          this.feedback = "Invalid credentials";
          if(err.status==0) this.feedback = "Could not connect to the server, please try again later.";
        }
      });
    }, 100);
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    let ret = emailRegex.test(email);
    return ret;
  }

  goBack() {
    this.feedback = "";
    this.router.navigate(['']);
  }
}
