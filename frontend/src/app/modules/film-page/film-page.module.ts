import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FilmPageRoutingModule } from './film-page-routing.module';
import { FilmPageComponent } from './film-page/film-page.component';
import { FilmPlayerComponent } from './film-player/film-player.component';
import {SharedModule} from "../../shared/shared.module";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    FilmPageComponent,
    FilmPlayerComponent
  ],
    imports: [
        CommonModule,
        FilmPageRoutingModule,
        SharedModule,
        ReactiveFormsModule
    ]
})
export class FilmPageModule { }
