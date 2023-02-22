import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MyConfig} from "../my-config";
declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  title: string = 'CertigonTask';
  filter_username: string = '';
  usersData: any;
  selectedUser: any = null;


  constructor(private httpClient: HttpClient, private router: Router) { }

  getData(): void {
    this.httpClient.get(MyConfig.adresa_servera + '/KorisnickiNalog/GetAll', MyConfig.http_opcije()).subscribe((x: any) => {
        this.usersData = x;
      });
  }

  ngOnInit(): void {
    this.getData();
  }

  filter() {
    if (this.usersData == null)
      return [];
    return this.usersData.filter((x: any)=> x.korisnickoIme.length==0 || (x.korisnickoIme + " " + x.email).toLowerCase().startsWith(this.filter_username.toLowerCase()) || (x.email + " " + x.korisnickoIme).toLowerCase().startsWith(this.filter_username.toLowerCase()));
  }

  deleteUser(user: any) {
    this.httpClient.post(MyConfig.adresa_servera + "/User/Delete/" + user.id, null, MyConfig.http_opcije())
      .subscribe((res: any) => {
        let index = this.usersData.indexOf(user);
        if (index > -1) {
          this.usersData.splice(user, 1);
          this.getData();
        }
      })
    porukaSuccess(`User deleted successfully!`)
  }

  editUser(user: any){
    this.selectedUser = user;
    this.selectedUser.name = "Edit user";
    this.selectedUser.show = true;
  }
}
