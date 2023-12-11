import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { BaseModalComponent } from './components/base-modal/base-modal.component';
import { LoginComponent } from './components/login/login.component';
import { InputComponent } from './components/input/input.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";
import { AvatarComponent } from './components/avatar/avatar.component';
import { PosterComponent } from './components/poster/poster.component';
import { FilmDetailsComponent } from './components/film-details/film-details.component';
import { CustomSelectComponent } from './components/custom-select/custom-select.component';
import { CustomMultiSelectComponent } from './components/custom-multi-select/custom-multi-select.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { FooterComponent } from './components/footer/footer.component';



@NgModule({
  declarations: [
    HeaderComponent,
    BaseModalComponent,
    LoginComponent,
    InputComponent,
    AvatarComponent,
    PosterComponent,
    FilmDetailsComponent,
    CustomSelectComponent,
    CustomSelectComponent,
    CustomMultiSelectComponent,
    PaginationComponent,
    FooterComponent
  ],
  exports: [
    HeaderComponent,
    InputComponent,
    AvatarComponent,
    BaseModalComponent,
    PosterComponent,
    FilmDetailsComponent,
    CustomSelectComponent,
    CustomSelectComponent,
    CustomMultiSelectComponent,
    PaginationComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    NgOptimizedImage,
    FormsModule
  ]
})
export class SharedModule { }
