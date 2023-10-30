import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { SignupViewComponent } from './signup-view/signup-view.component';

const routes: Routes = [
  {path:'', component:ProductViewComponent},
  {path:'signup', component:SignupViewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
