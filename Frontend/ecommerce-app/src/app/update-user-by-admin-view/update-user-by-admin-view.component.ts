import { Component, OnInit } from '@angular/core';
import { userRetrieveModel } from '../signup-view/signupUserModel';
import { UpdateUserService } from '../update-user.service';
import { modifyUserByAdminModel } from './updateUserByAdminModel';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-update-user-by-admin-view',
  templateUrl: './update-user-by-admin-view.component.html',
  styleUrls: []
})

export class UpdateUserByAdminViewComponent implements OnInit {
  updatingUser: modifyUserByAdminModel;
  userId: string;
  feedback: string = "";
  selectedRoles: string[] = [];
  roles: string[] = [];

  constructor(private dataService: UpdateUserService, private route: Router, private api: ApiService) {
    if(!this.api.currentSession?.user.roles.includes('admin')) this.route.navigate(['']);
    const incomingData = dataService.getData();
    if (!!incomingData) {
      this.updatingUser = new modifyUserByAdminModel(incomingData?.name, incomingData?.address, incomingData?.roles)
      this.userId = incomingData?.guid;
    } else {
      this.updatingUser = new modifyUserByAdminModel("", "", []);
      this.userId = "";
    }
  }
  
  ngOnInit(): void {
    const incomingData = this.dataService.getData();
    this.getAllRoles();
    if (incomingData?.name) this.updatingUser.name = incomingData?.name;
    if (incomingData?.address) this.updatingUser.address = incomingData?.address;
    if (incomingData?.roles) this.selectedRoles = incomingData?.roles;
  }

  updateUserData() {
    this.feedback = "Loading...";
    this.updatingUser.roles = this.selectedRoles;
    console.log('cargando:', this.feedback)
    if (!this.updatingUser.password) this.updatingUser.password = "";
    this.api.putUserByAdmin(this.userId, this.updatingUser).subscribe(
      res => {
        this.feedback = "Successfully changed";
        if(this.api.currentSession?.user.guid == this.userId) {
          const updatedSession = this.api.currentSession;
          updatedSession.user = res;
          updatedSession.token = this.api.currentSession?.token;
          localStorage.setItem('user', JSON.stringify(updatedSession));
      }},
      err => {
        if (err.status == 0) this.feedback = "Could not connect to the server, please try again later.";
        else this.feedback = "An error has occured, please try again later.";
      }
    );
  }

  getAllRoles() {
    this.feedback = "Loading roles...";
    this.api.getRoles().subscribe(res => {
      this.roles = res.filter(r => !!r);
      this.feedback = "";
    },
      err => {
        if (err.status == 0) this.feedback = "Could not connect to the server, please try again later.";
        else this.feedback = "An error has occurred, please try again later.";
      }
    );
  }

  toggleRoleSelection(rol: string) {
    if (this.selectedRoles.includes(rol)) {
      this.selectedRoles = this.selectedRoles.filter(r => r !== rol);
    } else {
      this.selectedRoles.push(rol);
    }
  }

  goBack() {
    this.route.navigate(['/admin/users']);
  }
}


