import { Component, Input, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MyConfig} from "../../my-config";


declare function porukaSuccess(s: string): any;

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  @Input() editUser:any;

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  saveChanges(){
    this.httpClient.post(MyConfig.adresa_servera + "/KorisnickiNalog/Update/" + this.editUser.id, this.editUser, MyConfig.http_opcije())
      .subscribe((res:any) => {
        porukaSuccess(`User updated successfully`);
        this.editUser.show=false;
      })
  }
}
