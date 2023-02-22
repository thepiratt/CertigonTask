

export class LoginInformation {
  autentifikacijaToken:        AutentifikacijaToken=null;
  isLogiran:                   boolean=false;
  isPermissionManager:         boolean=false;
  isPermsijaAdmin:             boolean=false;
}

export interface AutentifikacijaToken {
  id:                   number;
  vrijednost:           string;
  korisnickiNalogId:    number;
  korisnickiNalog:      KorisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa:             string;
}

export interface KorisnickiNalog {
  id:                 number;
  korisnickoIme:      string;
  email:              string;
  isAdmin:            boolean;
  isManager:          boolean;

}
