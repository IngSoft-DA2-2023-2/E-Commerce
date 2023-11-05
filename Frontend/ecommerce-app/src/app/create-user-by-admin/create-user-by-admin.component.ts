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

constructor(private api: ApiService, private router: Router){
  this.creatingUser = new createUserByAdminModel("","", "", []);
}

createUserData(){
  this.creatingUser.roles=this.creatingUser.roles.toString().split(',');
  this.api.postUserByAdmin(this.creatingUser).subscribe(
    res => {
      this.feedback="Successfully created";
    },
    err => {
     this.feedback = "Not valid data"
    }
  );
}

goBack() {
  this.router.navigate(['/admin']);
}
}
/*import { Component } from '@angular/core';
import { userRetrieveModel } from '../signup-view/signupUserModel';
import { UpdateUserService } from '../update-user.service';
import { updateUserByAdminModel } from './updateUserByAdminModel';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';

@Component({
  selector: 'app-update-user-by-admin-view',
  templateUrl: './update-user-by-admin-view.component.html',
  styleUrls: ['./update-user-by-admin-view.component.css']
})

export class UpdateUserByAdminViewComponent {
  updatingUser: updateUserByAdminModel;
  userId: string;
  feedback: string = "";
  constructor(private dataService: UpdateUserService,private route: Router, private api: ApiService) {
    const incomingData = dataService.getData();
    if (!!incomingData) {
      this.updatingUser = new updateUserByAdminModel(incomingData?.name, incomingData?.address, incomingData?.roles)
      this.userId = incomingData?.guid;
    } else{
      this.updatingUser = new updateUserByAdminModel("", "", []);
      this.userId = "";
    }
  }
    ngOnInit(): void {
      const incomingData = this.dataService.getData();
      if(incomingData?.name)this.updatingUser.name = incomingData?.name;
      if(incomingData?.address)this.updatingUser.address = incomingData?.address;
      if(incomingData?.roles)this.updatingUser.roles = incomingData?.roles;
      console.log('en ngOnInit', this.updatingUser);
    }

    updateUserData(){
      this.updatingUser.roles=this.updatingUser.roles.toString().split(',');
      console.log('en updateUserData', this.updatingUser,this.dataService.getData()	)
      this.api.putUserByAdmin(this.userId,this.updatingUser).subscribe(
        res => {
          this.feedback="Successfully changed";
        },
        err => {
          this.feedback = "Not valid data"
        }
      );


    }

    goBack() {
      this.route.navigate(['/admin']);
    }
  }


*/