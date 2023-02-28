import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import { ItemsComponent } from './items/items.component';
import { EditItemComponent } from './items/edit-item/edit-item.component';
import {AuthorizationLoginCheck} from "./_guards/authorization-login-check.service";
import {AuthorizationAdminCheck} from "./_guards/authorization-admin-check.service";
import { UsersComponent } from './users/users.component';
import { EditUserComponent } from './users/edit-user/edit-user.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    NavComponent,
    ItemsComponent,
    EditItemComponent,
    UsersComponent,
    EditUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    AuthorizationLoginCheck,
    AuthorizationAdminCheck
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
