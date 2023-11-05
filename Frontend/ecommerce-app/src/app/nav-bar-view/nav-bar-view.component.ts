import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav-bar-view',
  templateUrl: './nav-bar-view.component.html',
  styleUrls: ['./nav-bar-view.component.css']
})
export class NavBarViewComponent implements OnInit {
  userName: string = "";

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    if(this.isLogged())this.userName =this.api.currentSession?.user.name || "";
  }

  openSignUpMenu() {
    this.router.navigate(['/signup']);
  }
  openSignInMenu() {
    this.router.navigate(['/signin']);
  }
  seeLoggedInfo() {
    console.log(this.api.currentSession);
  }

  isLogged(): boolean {
    return !!this.api.currentSession;
  }

}