import {HttpHeaders} from "@angular/common/http";
import {AuthService} from "./_helpers/authService";
import {AuthenticationToken} from "./_helpers/login-information";

export class MyConfig{
  static adresa_servera = "http://localhost:5000";
  static http_options= function (){

    let authenticationToken:AuthenticationToken = AuthService.getLoginInfo().authenticationToken;
    let mytoken = "";

    if (authenticationToken!=null)
      mytoken = authenticationToken.value;
    return {
        headers: {
        'authentication-token': mytoken,
      }
    };
  }

}
