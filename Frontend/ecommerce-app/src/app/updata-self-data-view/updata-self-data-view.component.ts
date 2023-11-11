import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { UpdataSelfDataModel } from './updateSelfDataModel';
import { userRetrieveModel } from '../signup-view/signupUserModel';
import { sessionModel } from '../signup-view/sessionModel';

@Component({
  selector: 'app-updata-self-data-view',
  templateUrl: './updata-self-data-view.component.html',
  styleUrls: []
})
export class UpdataSelfDataViewComponent {

  updatingUser: UpdataSelfDataModel;
  feedback: string = "";

  constructor(private api: ApiService, private router: Router) {
    const user=this.api.currentSession?.user;
    this.updatingUser = new UpdataSelfDataModel(user?.name || "", "", user?.address || "");
  }

  updateUserData() {
    console.log(this.updatingUser)
    this.api.putUserByThemself(this.updatingUser).subscribe({
      next: res => {
        let user= (JSON.parse(localStorage.getItem('user') || "") as sessionModel);
        user.user = (res as userRetrieveModel);
        localStorage.setItem('user', JSON.stringify(user));
        this.feedback = "Successfully updated";
      },
      error: err => {
        this.feedback = "Not valid data"
      }
    });
  }

  goBack(){
    this.router.navigate(['']);
  }
}