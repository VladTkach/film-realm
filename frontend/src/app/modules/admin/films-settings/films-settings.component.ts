import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../core/base/base.component";
import {FilmService} from "../../../core/services/film.service";
import {takeUntil} from "rxjs";
import {ActorDto} from "../../../models/actor/actor-dto";
import {FilmDto} from "../../../models/film/film-dto";
import {Pagination} from "../../../models/pagination-filter-sort/pagination";
import {FilterOption} from "../../../models/pagination-filter-sort/filter-option";

@Component({
  selector: 'app-films-settings',
  templateUrl: './films-settings.component.html',
  styleUrls: ['./films-settings.component.sass']
})
export class FilmsSettingsComponent extends BaseComponent implements OnInit{
  public isFilmModalOpen = false;
  public films: FilmDto[] = [];
  public selectedFilm?: FilmDto;
  public pagination?: Pagination;
  public filterOptions: FilterOption = new FilterOption();

  constructor(private filmService: FilmService) {
    super();
  }

  ngOnInit() {
    if (!this.pagination){
      return;
    }

    this.filmService.getAllFilms(this.pagination, this.filterOptions)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: pagedFilms => {
          this.films = pagedFilms.items;
        }
      })
  }

  public openFilmModal(isCreateMode: boolean) {
    this.isFilmModalOpen = true;
  }

  public closeFilmModal() {
    this.isFilmModalOpen = false;
  }

  public filmReceive(film: FilmDto) {
    this.films.push(film)
  }

  public isSelected(film: FilmDto) {
    return this.selectedFilm === film;
  }

  public toggleSelection(film: FilmDto) {
    if (this.selectedFilm === film) {
      this.selectedFilm = undefined;
    } else {
      this.selectedFilm = film;
    }
  }

  public deleteFilm() {
    if (!this.selectedFilm) {
      return;
    }

    this.filmService.deleteFilm(this.selectedFilm.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: () => {
          const filmIndex = this.films.findIndex(f => f.id === this.selectedFilm?.id);
          if (filmIndex != -1) {
            this.films.splice(filmIndex, 1);
            this.selectedFilm = undefined;
          }
        }
      });
  }
}
