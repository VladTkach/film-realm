import {Injectable} from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {NewFilmDto} from "../../models/film/new-film-dto";
import {FilmDto} from "../../models/film/film-dto";
import {PagedFilms} from "../../models/film/paged-films";
import {Pagination} from "../../models/pagination-filter-sort/pagination";
import {FilterOption} from "../../models/pagination-filter-sort/filter-option";
import {QueryParams} from "../../models/pagination-filter-sort/query-params";

@Injectable({
  providedIn: 'root'
})
export class FilmService {
  private readonly authRoutePrefix = '/api/film';

  constructor(private http: HttpInternalService) {
  }

  public getAllFilms(pagination: Pagination, filters: FilterOption, sortBy?: string) {
    return this.http.getRequest<PagedFilms>(`${this.authRoutePrefix}/all`, this.getQueryParams(pagination, filters, sortBy));
  }

  public getFilmById(filmId: number) {
    return this.http.getRequest<FilmDto>(`${this.authRoutePrefix}/${filmId}`);
  }

  public addFilm(newFilmDto: NewFilmDto) {
    const formData = new FormData();
    formData.append('name', newFilmDto.name);
    formData.append('description', newFilmDto.description);
    formData.append('year', newFilmDto.year.toString());
    formData.append('poster', newFilmDto.poster);
    formData.append('resourceUrl', newFilmDto.resourceUrl);
    formData.append('genresId', newFilmDto.genresId.toString());
    formData.append('actorsId', newFilmDto.actorsId.toString());

    return this.http.postRequest<FilmDto>(this.authRoutePrefix, formData);
  }

  public deleteFilm(filmId: number) {
    return this.http.deleteRequest(`${this.authRoutePrefix}/${filmId}`);
  }

  public getQueryParams(paginationParams: Pagination, filters: FilterOption, sortBy?: string) {
    let filterParams = '';
    let sorts = '';
    if (sortBy){
      sorts += `${sortBy}`
    }
    if (filters.search) {
      filterParams += `name@=${filters.search},`;
    }

    if (filters.genres && filters.genres.length !== 0){
      const genreNames = filters.genres.map(genre => genre.name).join('|');
      filterParams += `FilmIsAnyOf==${genreNames},`;
    }



    const queryParams: QueryParams = {
      sorts: sorts,
      page: paginationParams.page,
      pageSize: paginationParams.pageSize,
      filters: filterParams
    }

    return queryParams;
  }
}
