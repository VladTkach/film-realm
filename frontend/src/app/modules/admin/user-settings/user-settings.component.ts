import { Component } from '@angular/core';
import {takeUntil} from "rxjs";
import {BaseComponent} from "../../../core/base/base.component";
import {UserDto} from "../../../models/user/user-dto";
import {AdminService} from "../../../core/services/admin.service";
import {AuthService} from "../../../core/services/auth.service";

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.sass']
})
export class UserSettingsComponent extends BaseComponent{
  public isUserModalOpen = false;
  public admins: UserDto[] = [];
  public selectedAdmin?: UserDto;

  constructor(private adminService: AdminService, public authService: AuthService) {
    super();
  }

  ngOnInit() {
    this.adminService.getAdmins()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: admins => {
          this.admins = admins;
        }
      });
  }

  public openUserModal() {
    this.isUserModalOpen = true;
  }

  public closeUserModal() {
    this.isUserModalOpen = false;
  }

  public adminReceive(admin: UserDto) {
      this.admins?.push(admin);
      this.closeUserModal();
  }

  public isSelected(admin: UserDto) {
    return this.selectedAdmin === admin;
  }

  public toggleSelection(admin: UserDto) {
    if (this.selectedAdmin === admin) {
      this.selectedAdmin = undefined;
    } else {
      this.selectedAdmin = admin;
    }
  }

  public deleteAdmin() {
    if (!this.selectedAdmin) {
      return;
    }

    this.adminService.deleteAdmin(this.selectedAdmin.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: () => {
          const adminIndex = this.admins.findIndex(a => a.id === this.selectedAdmin?.id);
          if (adminIndex != -1) {
            this.admins.splice(adminIndex, 1);
            this.selectedAdmin = undefined;
          }
        }
      });
  }
}
