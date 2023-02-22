import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {AutorizacijaLoginProvjera} from "./_guards/authorization-login-check.service";
import {AutorizacijaAdminProvjera} from "./_guards/authorization-admin-check.service";
import {ItemsComponent} from "./items/items.component";
import {UsersComponent} from "./users/users.component";

const routes: Routes = [
  {path: 'items', component: ItemsComponent, canActivate: [AutorizacijaLoginProvjera]},
  {path:'', component: HomeComponent, canActivate: [AutorizacijaLoginProvjera]},
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent},
  {path:'users', component: UsersComponent, canActivate:[AutorizacijaAdminProvjera]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AutorizacijaLoginProvjera,AutorizacijaAdminProvjera]
})
export class AppRoutingModule { }
