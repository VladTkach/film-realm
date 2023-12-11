import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FilmDto} from "../../../models/film/film-dto";
import {Router} from "@angular/router";

@Component({
  selector: 'app-film-details',
  templateUrl: './film-details.component.html',
  styleUrls: ['./film-details.component.sass'],
})
export class FilmDetailsComponent {
  @Input() film?: FilmDto;

  @Output() selectFilm: EventEmitter<void> = new EventEmitter<void>();

  constructor(private route: Router) {
  }
  public selectFilmHandler() {
    this.route.navigateByUrl(`/films/${this.film?.id}`)
  }
}
