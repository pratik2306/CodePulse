import { Inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from "jwt-decode";

export const authGuard: CanActivateFn = (route, state) => {

  // Check For Jwt Token
  const cookieService  = Inject(CookieService)
  //const authService = Inject(AuthService)
  // const router = Inject(Router)

  //var token = cookieService.get("Authorization");

  // if(!token){
  //   authService.logout()
  //   return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } })
  // }

  // token = token.replace('Bearer ','')
  // const decodedToken: any = jwtDecode(token)

  // const expirationDate = decodedToken.exp * 1000;
  // const currentTime = new Date().getTime()

  // if(expirationDate < currentTime){
  //   authService.logout()
  //   return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } })
  // }

  // const user = authService.getUser()

  // if(!user || !user.roles.include('Writer')){
  //   alert("UnAuthorized");
  //   return false;
  // }

  return true;
};

