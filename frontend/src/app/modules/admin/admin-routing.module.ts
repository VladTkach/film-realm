import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AdminPanelComponent} from "./admin-panel/admin-panel.component";
import {FilmsSettingsComponent} from "./films-settings/films-settings.component";
import {GenresSettingsComponent} from "./genres-settings/genres-settings.component";
import {ActorsSettingsComponent} from "./actors-settings/actors-settings.component";
import {UserSettingsComponent} from "./user-settings/user-settings.component";

const routes: Routes = [
  {
    path: '',
    component: AdminPanelComponent,
    children: [
      {
        path: 'films',
        component: FilmsSettingsComponent
      },
      {
        path: 'genres',
        component: GenresSettingsComponent
      },
      {
        path: 'actors',
        component: ActorsSettingsComponent
      },
      {
        path: 'users',
        component: UserSettingsComponent
      },
      { path: '', pathMatch: 'full', redirectTo: 'films' }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
