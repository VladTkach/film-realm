import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { MenuComponent } from './menu/menu.component';
import { GenresSettingsComponent } from './genres-settings/genres-settings.component';
import { FilmsSettingsComponent } from './films-settings/films-settings.component';
import { ActorsSettingsComponent } from './actors-settings/actors-settings.component';
import { GenreCardComponent } from './genres-settings/genre-card/genre-card.component';
import { GenreModalComponent } from './genres-settings/genre-modal/genre-modal.component';
import {SharedModule} from "../../shared/shared.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { ActorCardComponent } from './actors-settings/actor-card/actor-card.component';
import { ActorModalComponent } from './actors-settings/actor-modal/actor-modal.component';
import { FilmModalComponent } from './films-settings/film-modal/film-modal.component';
import {NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import { FilmCardComponent } from './films-settings/film-card/film-card.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { UserCardComponent } from './user-settings/user-card/user-card.component';
import { UserModalComponent } from './user-settings/user-modal/user-modal.component';


@NgModule({
  declarations: [
    AdminPanelComponent,
    MenuComponent,
    GenresSettingsComponent,
    FilmsSettingsComponent,
    ActorsSettingsComponent,
    GenreCardComponent,
    GenreModalComponent,
    ActorCardComponent,
    ActorModalComponent,
    FilmModalComponent,
    FilmCardComponent,
    UserSettingsComponent,
    UserCardComponent,
    UserModalComponent
  ],
  exports: [
    FilmCardComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    FormsModule
  ]
})
export class AdminModule { }
