

export class LoginInformation {
  authenticationToken:        AuthenticationToken=null;
  isLogiran:                   boolean=false;
  isPermissionManager:         boolean=false;
  isPermissionAdmin:             boolean=false;
}

export interface AuthenticationToken {
  id:                   number;
  value:           string;
  userAccountId:    number;
  userAccount:      UserAccount;
  created_time: Date;
  ipAdress:             string;
}

export interface UserAccount {
  id:                 number;
  userName:      string;
  email:              string;
  isAdmin:            boolean;
  isManager:          boolean;

}
