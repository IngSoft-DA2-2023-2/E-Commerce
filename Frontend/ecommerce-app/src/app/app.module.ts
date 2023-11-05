import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'; // <-- Import HttpClientModule
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { SignupViewComponent } from './signup-view/signup-view.component';
import { SigninViewComponent } from './signin-view/signin-view.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { NavBarViewComponent } from './nav-bar-view/nav-bar-view.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { UpdateProductViewComponent } from './update-product-view/update-product-view.component';
import { UpdateUserByAdminViewComponent } from './update-user-by-admin-view/update-user-by-admin-view.component';
import { FormsModule } from '@angular/forms';
import { CreateUserByAdminComponent } from './create-user-by-admin/create-user-by-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    SignupViewComponent,
    SigninViewComponent,
    AdminViewComponent,
    NavBarViewComponent,
    LandingPageComponent,
    UpdateProductViewComponent,
    UpdateUserByAdminViewComponent,
    CreateUserByAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
