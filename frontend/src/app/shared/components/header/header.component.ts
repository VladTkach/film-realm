import { Component } from '@angular/core';
import {AuthService} from "../../../core/services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent {
  public isLoginModalOpen = false;

  constructor(public authService: AuthService) {

  }

  public openLoginModal() {
    this.isLoginModalOpen = true;
  }

  public closeLoginModal() {
    this.isLoginModalOpen = false;
  }
}
