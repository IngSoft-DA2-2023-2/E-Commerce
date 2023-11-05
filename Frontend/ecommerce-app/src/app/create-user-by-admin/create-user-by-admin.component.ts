import { Component } from '@angular/core';
import { modifyUserByAdminModel } from '../update-user-by-admin-view/updateUserByAdminModel';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createUserByAdminModel } from './createUserByAdminModel';


@Component({
  selector: 'app-create-user-by-admin',
  templateUrl: './create-user-by-admin.component.html',
  styleUrls: ['./create-user-by-admin.component.css']
})
export class CreateUserByAdminComponent {
  creatingUser: createUserByAdminModel;
  feedback: string = "";

  constructor(private api: ApiService, private router: Router) {
    this.creatingUser = new createUserByAdminModel("", "", "", []);
  }


  createUserData() {
    this.creatingUser.roles = this.creatingUser.roles.toString().split(',');
    this.api.postUserByAdmin(this.creatingUser).subscribe({
      next: res => {
        this.feedback = "Successfully created";
      },
      error: err => {
        this.feedback = "Not valid data"
      }
    });
  }





  goBack() {
    this.router.navigate(['/admin']);
  }
}