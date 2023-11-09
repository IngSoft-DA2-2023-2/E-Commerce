import { Component, ChangeDetectorRef } from '@angular/core';
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


  constructor(private api: ApiService, private router: Router, private cdr: ChangeDetectorRef) {
    if(!this.api.currentSession?.user.roles.includes('admin')) this.router.navigate(['']);
    this.creatingUser = new createUserByAdminModel("", "", "", []);
    this.getAllRoles();
  }

  loading: boolean=false;
  isLoading(): boolean {
    return this.loading;
  }
  createUserData() {
    this.loading=true;
    console.log('cargando:', this.feedback);
    this.creatingUser.roles = this.selectedRoles;

    console.log('Before HTTP request, vale:', this.feedback);
    let that=this;
    this.api.postUserByAdmin(this.creatingUser).subscribe(
      res => {
        that.feedback= "Successfully created"
        that.loading=false;
        console.log('HTTP request success');
        that.feedback = "Successfully created";
        that.creatingUser = new createUserByAdminModel("", "", "", []);
        that.selectedRoles = [];
      },
      err => {
        debugger;
        console.log('el error fue:', err);
        if (err.status == 0) that.feedback = "Could not connect to the server, please try again later.";
        else if (err.status == 400){
          if(!!err.error.errorMessage)that.feedback = err.error.errorMessage
          else if(!!err.error.title)that.feedback=err.error.title;
        } 
        else that.feedback = "Not valid data";
        that.loading=false;
        console.log('fb',that.feedback)
      }
    );
  }

  getAllRoles() {
    this.feedback = "Loading roles...";
    this.api.getRoles().subscribe(res => {
      this.roles = res.filter(r => !!r);
      this.feedback = "";
    }, err => {
      if (err.status == 0) this.feedback = "Could not connect to the server, please try again later.";
      else this.feedback = "An error has occured, please try again later.";
    }
    )
  }

  toggleRoleSelection(rol: string) {
    if (this.selectedRoles.includes(rol)) {
      this.selectedRoles = this.selectedRoles.filter(r => r !== rol);
    } else {
      this.selectedRoles.push(rol);
    }
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    let ret = emailRegex.test(email);
    return ret;
  }

  goBack() {
    this.router.navigate(['/admin/users']);
  }
}