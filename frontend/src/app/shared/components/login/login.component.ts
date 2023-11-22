import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../core/services/auth.service";
import {UserLoginDto} from "../../../models/user/user-login-dto";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  @Output() close = new EventEmitter();
  public loginForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private authService: AuthService) {
  }

  public closeModal() {
    this.close.emit();
  }

  public ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.loginForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50)],
      ],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
    });
  }

  public login() {
    const user: UserLoginDto = this.loginForm.value;

    this.authService.login(user)
      .subscribe({
        next: () => {
          this.closeModal();
        }
      })
  }
}
