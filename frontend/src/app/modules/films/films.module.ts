import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FilmsRoutingModule } from './films-routing.module';
import { FilmPanelComponent } from './film-panel/film-panel.component';
import {SharedModule} from "../../shared/shared.module";
import {AdminModule} from "../admin/admin.module";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    FilmPanelComponent
  ],
  imports: [
    CommonModule,
    FilmsRoutingModule,
    SharedModule,
    AdminModule,
    ReactiveFormsModule
  ]
})
export class FilmsModule { }
