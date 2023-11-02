import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupViewComponent } from './signup-view/signup-view.component';
import { SigninViewComponent } from './signin-view/signin-view.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { UpdateProductViewComponent } from './update-product-view/update-product-view.component';

const routes: Routes = [
  {path:'', component:LandingPageComponent},
  {path:'signup', component:SignupViewComponent},
  {path:'signin', component:SigninViewComponent},
  {path:'admin', component:AdminViewComponent},
  {path:'admin/updateProduct', component:UpdateProductViewComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
