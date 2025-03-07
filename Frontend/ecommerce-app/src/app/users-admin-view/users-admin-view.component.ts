import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { UpdateUserService } from '../update-user.service';
import { userRetrieveModel } from '../signup-view/signupUserModel';

@Component({
  selector: 'app-users-admin-view',
  templateUrl: './users-admin-view.component.html',
  styleUrls: ['./users-admin-view.component.css']
})
export class UsersAdminViewComponent {

  constructor(private api: ApiService, private router: Router, private userService: UpdateUserService) {
    if(!this.api.currentSession?.user.roles.includes('admin')) this.router.navigate(['']);
    this.api.updateSession();
    this.getUsers();
  }

  feedback: string = "";
  loading: boolean = false;
  users: userRetrieveModel[] = [];

  getUsers() {
    this.api.getUsers().subscribe({
      next: res => {
        this.users = res;
      },
      error: err => {
        this.feedback = "Could not get users";
        this.users = [];
      }
    }
    );
  }

  updateUser(u: userRetrieveModel) {
    this.userService.setData(u);
    this.router.navigate(['admin/updateUser']);
  }

  deleteUser(u: userRetrieveModel) {
    this.loading=true;
    this.api.deleteUsers(u.guid).subscribe({
      next: res => {
        this.getUsers();
        this.feedback = "User deleted";
        this.loading=false;
      },
      error: err => {
        if(err.status==0) this.feedback = "Could not connect to server";
        else this.feedback = "Could not delete user";
        this.loading=false;
      }
    }
    );
  }

  isLoading(){
    return this.loading;
  }
  
  createUser() {
    this.router.navigate(['admin/createUser']);
  }

  goBack() {
    this.router.navigate(['']);
  }
}

