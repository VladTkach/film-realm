import {Component, OnInit} from '@angular/core';
import {AuthService} from "./core/services/auth.service";
import {UserService} from "./core/services/user.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit{
  constructor(private authService: AuthService, private userService: UserService) {
  }
  ngOnInit() {
    this.setCurrentUser();
  }

  private setCurrentUser(){
    this.userService.getUserFromToken()
      .subscribe({
        next: user => {
          this.authService.setCurrentUser(user);
        }
      });
  }
}
