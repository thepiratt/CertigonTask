import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { Router } from '@angular/router';
import {AuthService} from "../_helpers/authService";
import {LoginInformation} from "../_helpers/login-information";
import {MyConfig} from "../my-config";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  logoutButton() {
    AuthService.setLoginInfo(null);

      this.httpKlijent.post(MyConfig.adresa_servera + "/Autentifikacija/Logout/", null, MyConfig.http_opcije())
      .subscribe((x: any) => {
        this.router.navigateByUrl("/login");
        porukaSuccess("Logout successful");
      });
  }

  loginInfo():LoginInformation {
    return AuthService.getLoginInfo();
  }
}
