import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { sessionRequest } from '../signup-view/sessionModel';
import { Router } from '@angular/router';
import { user } from '../signup-view/signupUserModel';

@Component({
  selector: 'app-signin-view',
  templateUrl: './signin-view.component.html',
  styleUrls: ['./signin-view.component.css']
})
export class SigninViewComponent {

  constructor(private api:ApiService, private router:Router) { }

  feedback:string = "";

  signInUser(email:string,password:string){
    this.api.postSession(new sessionRequest(email,password)).subscribe(res=>{
      if(this.api.loggedUser == undefined){
      this.api.loggedUser = {token: "", user: new user()};
      }
      this.api.loggedUser.token = res.token;
      this.api.loggedUser.user = res.user;
 
      this.feedback="success";
      this.router.navigate(['']);
    },err=>{
      this.feedback="Invalid credentials";
    }
    );
}

goBack(){
  this.feedback="";
    this.router.navigate(['']);
}

}
