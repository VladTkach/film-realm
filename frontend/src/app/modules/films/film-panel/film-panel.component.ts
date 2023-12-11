import {Component, OnInit} from '@angular/core';
import {FilmService} from "../../../core/services/film.service";
import {BaseComponent} from "../../../core/base/base.component";
import {takeUntil} from "rxjs";
import {FormBuilder, FormGroup} from "@angular/forms";
import {PagedFilms} from "../../../models/film/paged-films";
import {Pagination} from "../../../models/pagination-filter-sort/pagination";
import {FilterOption} from "../../../models/pagination-filter-sort/filter-option";
import {GenreDto} from "../../../models/genre/genre-dto";
import {GenreService} from "../../../core/services/genre.service";

@Component({
  selector: 'app-film-panel',
  templateUrl: './film-panel.component.html',
  styleUrls: ['./film-panel.component.sass']
})
export class FilmPanelComponent extends BaseComponent implements OnInit{
  public pagedFilms?: PagedFilms;
  public filterForm : FormGroup = new FormGroup({});
  public pagination: Pagination = new Pagination(1, 10);
  public filterOptions: FilterOption = new FilterOption();
  public sorts: string[] = ['Name', 'Year'];
  public genres: GenreDto[] = [];

  constructor(private filmService: FilmService, private fb: FormBuilder, private genreService: GenreService) {
    super();
  }

  ngOnInit() {
    this.getFilms();
    this.getGenres();
    this.initializeForm();
  }

  public filterFilm() {
    this.filterOptions.search = this.filterForm.value['search'];
    this.filterOptions.genres = this.filterForm.value['genre'];
    console.log(this.filterForm.value);
    this.getFilms();
  }

  pageChanged(newPage: number) {
    if (!this.pagination){
      return;
    }
    this.pagination.page = newPage;
    this.getFilms();
  }

  private getFilms(){
    if (!this.pagination){
      return;
    }

    this.filmService.getAllFilms(this.pagination, this.filterOptions, this.filterForm.value['sort'])
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: pagedFilms => {
          this.pagedFilms = pagedFilms;
        }
      });
  }

  private initializeForm() {
    this.filterForm = this.fb.group({
      search: [''],
      sort: [],
      genre: [],
    });
  }

  private getGenres() {
    this.genreService.getAllGenres()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: genres => {
          this.genres = genres;
        }
      })
  }
}
