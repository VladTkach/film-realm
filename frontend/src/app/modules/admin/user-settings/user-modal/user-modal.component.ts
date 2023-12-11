import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {BaseComponent} from "../../../../core/base/base.component";
import {UserDto} from "../../../../models/user/user-dto";
import {AdminService} from "../../../../core/services/admin.service";
import {switchMap, takeUntil} from "rxjs";

@Component({
  selector: 'app-user-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.sass']
})
export class UserModalComponent extends BaseComponent implements OnInit{
  @Output() close = new EventEmitter();
  @Output() user = new EventEmitter<UserDto>();
  public userForm: FormGroup = new FormGroup({});
  public users: UserDto[] = [];
  public selectedUser?: UserDto;

  constructor(private fb: FormBuilder, private adminService: AdminService) {
    super();
  }

  public closeModal() {
    this.close.emit();
  }

  public ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.userForm = this.fb.group({
      search: ['']
    });
  }

  public promote() {
    if (!this.selectedUser){
      return;
    }

    this.adminService.promoteAdmin(this.selectedUser.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: admin => {
          this.user.emit(admin);
        }
      })
  }

  public onInputChange(userName: string) {
    if (userName.trim() === ''){
      return;
    }
    this.adminService.getUsersByName(userName)
      .pipe(
        takeUntil(this.unsubscribe$),
        switchMap(() => this.adminService.getUsersByName(userName))
      )
      .subscribe({
        next: users => {
          this.users = users;
        }
      });
  }

  public selectUser(user: UserDto) {
    if (this.isSelected(user)){
      this.selectedUser = undefined;
    }
    else {
      this.selectedUser = user;
    }
  }

  public isSelected(user: UserDto){
    return this.selectedUser === user;
  }
}
