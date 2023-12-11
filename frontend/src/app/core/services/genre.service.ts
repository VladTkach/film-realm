import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {UserDto} from "../../models/user/user-dto";
import {GenreDto} from "../../models/genre/genre-dto";
import {NewGenreDto} from "../../models/genre/new-genre-dto";
import {UpdateGenreDto} from "../../models/genre/update-genre-dto";

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private readonly authRoutePrefix = '/api/genre';
  constructor(private http: HttpInternalService) { }

  public getAllGenres() {
    return this.http.getRequest<GenreDto[]>(`${this.authRoutePrefix}/all`);
  }

  public addGenre(newGenreDto: NewGenreDto){
    return this.http.postRequest<GenreDto>(this.authRoutePrefix, newGenreDto);
  }

  public updateGenre(updateGenreDto: UpdateGenreDto){
    return this.http.putRequest<GenreDto>(this.authRoutePrefix, updateGenreDto);
  }

  public deleteGenre(genreId: number){
    return this.http.deleteRequest<GenreDto>(`${this.authRoutePrefix}/${genreId}`);
  }
}
