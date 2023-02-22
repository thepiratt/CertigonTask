﻿import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService} from "../_helpers/authService";


@Injectable()
export class AutorizacijaLoginProvjera implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        try {
          if (AuthService.getLoginInfo().isLogiran)
            return true;
        }catch (e) {
        }
        // not logged in so redirect to login page with the return url
        this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
