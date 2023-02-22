import {HttpHeaders} from "@angular/common/http";
import {AuthService} from "./_helpers/authService";
import {AutentifikacijaToken} from "./_helpers/login-information";

export class MyConfig{
  static adresa_servera = "http://localhost:5000";
  static http_opcije= function (){

    let autentifikacijaToken:AutentifikacijaToken = AuthService.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;
    return {
        headers: {
        'autentifikacija-token': mojtoken,
      }
    };
  }

}
