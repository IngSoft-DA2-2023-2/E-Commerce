import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { SignupViewComponent } from './signup-view/signup-view.component';
import { SigninViewComponent } from './signin-view/signin-view.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { LandingPageComponent } from './landing-page/landing-page.component';

const routes: Routes = [
  {path:'', component:LandingPageComponent},
  {path:'signup', component:SignupViewComponent},
  {path:'signin', component:SigninViewComponent},
  {path:'admin', component:AdminViewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
