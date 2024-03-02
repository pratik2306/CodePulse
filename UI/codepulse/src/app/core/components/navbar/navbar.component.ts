import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/features/auth/models/user.mode';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

 user? : User
  constructor(private authService: AuthService, private router: Router){

  }
  ngOnInit(): void {
    this.authService.user().subscribe({
      next: (res) => {
        this.user = res;
      }
    });

    this.user = this.authService.getUser();
  }

  onLogout():void {
    this.authService.logout()
    this.router.navigateByUrl('/')
  }
}
