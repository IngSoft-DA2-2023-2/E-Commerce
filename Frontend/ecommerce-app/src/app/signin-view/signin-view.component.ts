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

  constructor(private api:ApiService, private router:Router) { }

  feedback:string = "";

  signInUser(email:string,password:string){
    this.api.postSession(new sessionRequest(email,password)).subscribe(res=>{
      this.api.currentSession = res;
      this.feedback="success";
      localStorage.setItem('user',JSON.stringify(res));
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
