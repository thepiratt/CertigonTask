import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MyConfig} from "../my-config";
import {AuthService} from "../_helpers/authService";
import {LoginInformation} from "../_helpers/login-information";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  /*errorMessage!: string;*/

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username:'',
      password:''
    });
  }
  submit() {
    let sending = {
      UserName: this.form.value.username,
      Password: this.form.value.password
    };
    this.httpClient.post<LoginInformation>(MyConfig.adresa_servera + "/Authentication/Login/", sending)
      .subscribe((x: LoginInformation) => {
        if (x.isLogiran) {
          porukaSuccess("Login successful");
          AuthService.setLoginInfo(x)
          this.router.navigateByUrl("/items");

        } else {
          AuthService.setLoginInfo(null)
          porukaError("Unsuccessful login");
        }
      });
  }


  btnRegister() {
    this.router.navigate(['/register']);
  }
}
