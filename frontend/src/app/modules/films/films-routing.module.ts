import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {FilmPanelComponent} from "./film-panel/film-panel.component";

const routes: Routes = [
  {
    path: '',
    component: FilmPanelComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FilmsRoutingModule { }
