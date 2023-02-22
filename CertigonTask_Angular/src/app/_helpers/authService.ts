import {LoginInformation} from "./login-information";

export class AuthService {

  static setLoginInfo(x: LoginInformation):void
  {
    if (x==null)
      x = new LoginInformation();
    localStorage.setItem("autentifikacija-token", JSON.stringify(x));
  }

  static getLoginInfo():LoginInformation
  {
      let x = localStorage.getItem("autentifikacija-token");
      if (x==="")
        return new LoginInformation();

      try {
        let loginInformacije:LoginInformation = JSON.parse(x);
        loginInformacije.isPermsijaAdmin = loginInformacije.autentifikacijaToken.korisnickiNalog.isAdmin;
        loginInformacije.isPermissionManager = loginInformacije.autentifikacijaToken.korisnickiNalog.isManager;
        if (loginInformacije==null)
          return new LoginInformation();
        return loginInformacije;
      }
      catch (e)
      {
        return new LoginInformation();
      }
  }
}
