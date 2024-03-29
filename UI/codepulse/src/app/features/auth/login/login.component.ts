import { Component, OnDestroy } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
  model: LoginRequest;

  constructor(private authService: AuthService, private cookieService:CookieService, private router: Router){
    this.model = {
      email:'',
      password:''
    }
  }

  OnSubmit():void{
    this.authService.login(this.model).subscribe({
      next: (res) => {
        //Set Auth Cookie
        this.cookieService.set("Authorization",`Bearer ${res.token}`, undefined, '/', undefined, true,'Strict');
        this.authService.setUser(res);
        this.router.navigateByUrl('/')
      }
    })
  }
}
