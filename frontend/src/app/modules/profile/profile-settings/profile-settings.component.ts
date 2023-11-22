import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../core/services/auth.service";
import {UserDto} from "../../../models/user/user-dto";
import {UserService} from "../../../core/services/user.service";
import {UpdateUsernameDto} from "../../../models/user/update-username-dto";
import {UpdatePasswordDto} from "../../../models/user/update-password-dto";

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.sass']
})
export class ProfileSettingsComponent implements OnInit {
  public userNameForm: FormGroup = new FormGroup({});
  public passwordForm: FormGroup = new FormGroup({});
  public currentUser?: UserDto;

  private readonly maxFileLength = 5 * 1024 * 1024;
  private readonly allowedTypes = ['image/png', 'image/jpeg'];

  constructor(private fb: FormBuilder, private authService: AuthService, private userService: UserService) {
  }

  public ngOnInit() {
    this.getCurrentUser();
    this.initializeForm();
  }

  public updateUserName() {
    if (!this.currentUser) {
      return;
    }
    const updateUserNameDto: UpdateUsernameDto = {
      id: this.currentUser.id,
      userName: this.userNameForm.value['userName']
    }

    this.userService.updateUserName(updateUserNameDto)
      .subscribe({
        next: userDto => {
          this.authService.updateCurrentUser(userDto);
        }
      })
  }

  public updatePassword() {
    if (!this.currentUser) {
      return;
    }
    const updatePasswordDto: UpdatePasswordDto = {
      id: this.currentUser.id,
      password: this.passwordForm.value['currentPassword'],
      newPassword: this.passwordForm.value['newPassword']
    }

    this.userService.updatePassword(updatePasswordDto)
      .subscribe({
        next: userDto => {
          this.authService.updateCurrentUser(userDto);
        }
      })
  }

  public onFileChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;

    if (!inputElement?.files?.length) {
      return;
    }
    const file = inputElement.files[0];

    if (!this.fileValidate(file)) {
      return;
    }

    this.userService.addAvatar(file)
      .subscribe({
        next: () => {
          if (this.currentUser){
            this.currentUser.avatarUrl = this.currentUser.avatarUrl + '?' + Date.now();
          }
        }
      })
  }

  public fileValidate(file: File) {
    if (file.size > this.maxFileLength) {
      // this.notificationService.error(`The file size should not exceed ${this.maxFileLength / (1024 * 1024)}MB`);
      return false;
    }

    if (!this.allowedTypes.includes(file.type)) {
      // this.notificationService.error(`Invalid file type, need ${this.allowedTypes.join(', ')}`);
      return false;
    }

    return true;
  }

  private initializeForm() {
    this.userNameForm = this.fb.group({
      userName: [
        this.currentUser?.userName,
        [Validators.required, Validators.minLength(5), Validators.maxLength(50)],
      ]
    });

    this.passwordForm = this.fb.group({
      currentPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
      newPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
      confirmNewPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
    })
  }

  private getCurrentUser() {
    this.authService.currentUser$.subscribe({
      next: user => {
        if (user) {
          this.currentUser = user;
          this.currentUser.avatarUrl = this.currentUser.avatarUrl + '?' + Date.now();
        }
      }
    })
  }


}
