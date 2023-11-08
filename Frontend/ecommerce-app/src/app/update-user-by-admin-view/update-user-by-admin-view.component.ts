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
  roles: string[] =[];

  constructor(private dataService: UpdateUserService,private route: Router, private api: ApiService) {
    const incomingData = dataService.getData();
    if (!!incomingData) {
      this.updatingUser = new modifyUserByAdminModel(incomingData?.name, incomingData?.address, incomingData?.roles)
      this.userId = incomingData?.guid;
    } else{
      this.updatingUser = new modifyUserByAdminModel("", "", []);
      this.userId = "";
    }
  }
    ngOnInit(): void {
      const incomingData = this.dataService.getData();
      this.getAllRoles();
      if(incomingData?.name)this.updatingUser.name = incomingData?.name;
      if(incomingData?.address)this.updatingUser.address = incomingData?.address;
      if(incomingData?.roles)this.selectedRoles = incomingData?.roles;
    }

    updateUserData(){
      this.feedback = "";
      this.updatingUser.roles=this.selectedRoles;
      console.log('roles',this.updatingUser.roles)
      if(!this.updatingUser.password)this.updatingUser.password = "";
      this.api.putUserByAdmin(this.userId,this.updatingUser).subscribe(
        res => {
          this.feedback="Successfully changed";
        },
        err => {
          this.feedback = "Not valid data"
        }
      );
    }

    getAllRoles(){
      this.api.getRoles().subscribe(res => {
        this.roles = res.filter(r=>!!r);
        console.log('roles que llegaron:',this.roles)
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
      this.route.navigate(['/admin/users']);
    }
  }


