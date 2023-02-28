import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {AuthorizationLoginCheck} from "./_guards/authorization-login-check.service";
import {AuthorizationAdminCheck} from "./_guards/authorization-admin-check.service";
import {ItemsComponent} from "./items/items.component";
import {UsersComponent} from "./users/users.component";

const routes: Routes = [
  {path: 'items', component: ItemsComponent, canActivate: [AuthorizationLoginCheck]},
  {path:'', component: HomeComponent, canActivate: [AuthorizationLoginCheck]},
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent},
  {path:'users', component: UsersComponent, canActivate:[AuthorizationAdminCheck]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthorizationLoginCheck,AuthorizationAdminCheck]
})
export class AppRoutingModule { }
