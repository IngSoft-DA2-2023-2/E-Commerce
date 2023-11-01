import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav-bar-view',
  templateUrl: './nav-bar-view.component.html',
  styleUrls: ['./nav-bar-view.component.css']
})
export class NavBarViewComponent {
  constructor(private api: ApiService, private router: Router) { }

  openSignUpMenu() {
    this.router.navigate(['/signup']);
  }
  openSignInMenu() {
    this.router.navigate(['/signin']);
  }
  seeLoggedInfo() {
    console.log(this.api.currentSession);
  }
}