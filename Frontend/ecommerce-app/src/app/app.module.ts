import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { SignupViewComponent } from './signup-view/signup-view.component';
import { SigninViewComponent } from './signin-view/signin-view.component';
import { NavBarViewComponent } from './nav-bar-view/nav-bar-view.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { UpdateProductViewComponent } from './update-product-view/update-product-view.component';
import { UpdateUserByAdminViewComponent } from './update-user-by-admin-view/update-user-by-admin-view.component';
import { FormsModule } from '@angular/forms';
import { CreateUserByAdminComponent } from './create-user-by-admin/create-user-by-admin.component';
import { UsersAdminViewComponent } from './users-admin-view/users-admin-view.component';
import { ProductAdminViewComponent } from './product-admin-view/product-admin-view.component';
import { CreateProductAdminViewComponent } from './create-product-admin-view/create-product-admin-view.component';
import { PurchaseViewComponent } from './purchase-view/purchase-view.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    SignupViewComponent,
    SigninViewComponent,
    NavBarViewComponent,
    LandingPageComponent,
    UpdateProductViewComponent,
    UpdateUserByAdminViewComponent,
    CreateUserByAdminComponent,
    UsersAdminViewComponent,
    ProductAdminViewComponent,
    CreateProductAdminViewComponent,
    PurchaseViewComponent
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
