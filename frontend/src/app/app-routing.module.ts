import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('src/app/modules/home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'films',
    loadChildren: () => import('src/app/modules/films/films.module').then((m) => m.FilmsModule),
  },
  {
    path: 'list',
    loadChildren: () => import('src/app/modules/user-list/user-list.module').then((m) => m.UserListModule),
  },
  {
    path: 'profile',
    loadChildren: () => import('src/app/modules/profile/profile.module').then((m) => m.ProfileModule),
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
