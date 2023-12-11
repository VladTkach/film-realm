import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BaseComponent} from "../../../../core/base/base.component";
import {FilmDto} from "../../../../models/film/film-dto";
import {FilmService} from "../../../../core/services/film.service";
import {GenreDto} from "../../../../models/genre/genre-dto";
import {ActorDto} from "../../../../models/actor/actor-dto";
import {GenreService} from "../../../../core/services/genre.service";
import {ActorService} from "../../../../core/services/actor.service";
import {takeUntil} from "rxjs";
import {NewFilmDto} from "../../../../models/film/new-film-dto";

@Component({
  selector: 'app-film-modal',
  templateUrl: './film-modal.component.html',
  styleUrls: ['./film-modal.component.sass']
})
export class FilmModalComponent extends BaseComponent implements OnInit {
  @Output() close = new EventEmitter();
  @Output() film = new EventEmitter<FilmDto>();
  public filmForm: FormGroup = new FormGroup({});
  public posterUrl: string | ArrayBuffer | null = null;
  public currentPoster?: File;

  public genres: GenreDto[] = [];
  public selectedGenres: GenreDto[] = [];
  public actors: ActorDto[] = [];
  public selectedActors: ActorDto[] = [];

  public dropdownSettings: any = {};

  constructor(private fb: FormBuilder, private filmService: FilmService,
              private genreService: GenreService, private actorService: ActorService) {
    super();
  }

  public ngOnInit() {
    this.initializeDropdown();
    this.getGenres();
    this.getActors();
    this.initializeForm();
  }

  public closeModal() {
    this.close.emit();
  }

  private initializeForm() {
    this.filmForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(50)]],
      year: [2023, [Validators.required, Validators.maxLength(50)]],
      genres: [this.selectedGenres],
      actors: [this.selectedActors],
    });
  }

  public create() {
    if (!this.currentPoster){
      return;
    }
    const newFilm: NewFilmDto = {
      name: this.filmForm.value['name'],
      description: this.filmForm.value['description'],
      year: this.filmForm.value['year'],
      poster: this.currentPoster,
      resourceUrl: '12',
      genresId: this.filmForm.value['genres'].map((g: GenreDto) => g.id),
      actorsId: this.filmForm.value['actors'].map((a: ActorDto) => a.id)
    }

    this.filmService.addFilm(newFilm)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: newFilm => {
          this.film.emit(newFilm);
          this.closeModal();
        }
      })
  }
  public onFileChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;

    if (!inputElement?.files?.length) {
      return;
    }
    const file = inputElement.files[0];

    const mimeType = file.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }

    this.currentPoster = file;

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (_event) => {
      this.posterUrl = reader.result;
    }
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

  private getActors() {
    this.actorService.getAllActors()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: actors => {
          this.actors = actors;
        }
      })
  }

  private initializeDropdown() {
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: false
    };
  }


}
