﻿import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService} from "../_helpers/authService";


@Injectable()
export class AutorizacijaAdminProvjera implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        try {
          //nedovrseno privremeno rjesenje
          if (AuthService.getLoginInfo().isPermsijaAdmin)
            return true;
        }catch (e) {
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
