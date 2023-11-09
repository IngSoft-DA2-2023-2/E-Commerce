import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { Router } from '@angular/router';
import { product } from '../product-view/productModel';


@Component({
  selector: 'app-nav-bar-view',
  templateUrl: './nav-bar-view.component.html',
  styleUrls: []
})

export class NavBarViewComponent implements OnInit {
  userName: string = "";

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    const res = localStorage.getItem('user');
    if (!!res) this.api.currentSession = JSON.parse((res as string));
    if (this.isLogged()) this.userName = this.api.currentSession?.user.name || "";
  }

  logout() {
    if (!this.api.currentSession) return;
    const token: string = this.api.currentSession?.token;
    this.api.deleteSession().subscribe(
      response => {
        this.api.currentSession = undefined;
        localStorage.removeItem('user');
        this.router.navigate(['']);
      },
    );
  }

  countElemInCart():number{
    let elem = (JSON.parse(localStorage.getItem('cart') || "") as product[]).length;
   return elem;
  }

  openSignUpMenu() {
    this.router.navigate(['/signup']);
  }

  openSignInMenu() {
    this.router.navigate(['/signin']);
  }

  openAdminMenu() {
    this.router.navigate(['/admin']);
  }

  openUserAdminMenu() {
  this.router.navigate(['/admin/users']);
  }

  openProfileMenu(){
    this.router.navigate(['/profile']);
  }

  isAdmin(): boolean {
    return this.api.currentSession?.user?.roles.includes("admin") || false;
  }
  
  isBuyer(): boolean {
    return this.api.currentSession?.user?.roles.includes("buyer") || false;
  }

  isLogged(): boolean {
    return !!this.api.currentSession;
  }
  openPurchaseHistory(){
    this.router.navigate(['/purchases/history']);
  }

}