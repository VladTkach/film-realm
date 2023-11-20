import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { BaseModalComponent } from './components/base-modal/base-modal.component';
import { LoginComponent } from './components/login/login.component';
import { InputComponent } from './components/input/input.component';
import {ReactiveFormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";



@NgModule({
    declarations: [
        HeaderComponent,
        BaseModalComponent,
        LoginComponent,
        InputComponent
    ],
    exports: [
        HeaderComponent
    ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ]
})
export class SharedModule { }
