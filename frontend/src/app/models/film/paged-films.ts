import {FilmDto} from "./film-dto";

export interface PagedFilms{
  items: FilmDto[],
  totalCount: number
}
