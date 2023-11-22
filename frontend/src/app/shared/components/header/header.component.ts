import {Component, ElementRef, HostListener} from '@angular/core';
import {AuthService} from "../../../core/services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent {
  public isLoginModalOpen = false;

  public showDropdown = false;


  constructor(public authService: AuthService, private el: ElementRef) {

  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;

    // Перевірка, чи клік був здійснений поза меню
    if (!this.el.nativeElement.contains(target)) {
      this.showDropdown = false; // Сховати меню
    }
  }

  public toggleDropdown() {
    this.showDropdown = !this.showDropdown;
  }
  public openLoginModal() {
    this.isLoginModalOpen = true;
  }

  public closeLoginModal() {
    this.isLoginModalOpen = false;
  }

  public logout() {
    this.authService.logout();
  }
}
