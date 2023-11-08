import { Component } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { createUserByAdminModel } from './createUserByAdminModel';


@Component({
  selector: 'app-create-user-by-admin',
  templateUrl: './create-user-by-admin.component.html',
  styleUrls: []
})
export class CreateUserByAdminComponent {
  creatingUser: createUserByAdminModel;
  feedback: string = "";
  selectedRoles: string[] = [];
  roles: string[] = [];


  constructor(private api: ApiService, private router: Router) {
    this.creatingUser = new createUserByAdminModel("", "", "", []);
    this.getAllRoles();

  }

  createUserData() {
    this.creatingUser.roles = this.selectedRoles;
    this.api.postUserByAdmin(this.creatingUser).subscribe({
      next: res => {
        this.feedback = "Successfully created";
      },
      error: err => {
        this.feedback = "Not valid data"
      }
    });
  }

  getAllRoles() {
    this.api.getRoles().subscribe(res => {
      this.roles = res.filter(r => !!r);
    });
  }

  toggleRoleSelection(rol: string) {
    if (this.selectedRoles.includes(rol)) {
      this.selectedRoles = this.selectedRoles.filter(r => r !== rol);
    } else {
      this.selectedRoles.push(rol);
    }
  }

  goBack() {
    this.router.navigate(['/admin/users']);
  }
}