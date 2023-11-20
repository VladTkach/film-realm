import {Component, OnInit} from '@angular/core';
import {UserAuthDto} from "./models/user/user-auth-dto";
import {AuthService} from "./core/services/auth.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit{
  constructor(private authService: AuthService) {
  }
  ngOnInit() {
    this.setCurrentUser();
  }

  private setCurrentUser(){
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: UserAuthDto = JSON.parse(userString);
    this.authService.setCurrentUser(user);
  }
}
