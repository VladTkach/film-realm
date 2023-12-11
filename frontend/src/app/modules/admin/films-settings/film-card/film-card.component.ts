import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FilmDto} from "../../../../models/film/film-dto";

@Component({
  selector: 'app-film-card',
  templateUrl: './film-card.component.html',
  styleUrls: ['./film-card.component.sass']
})
export class FilmCardComponent {
  @Input() film?: FilmDto;
  @Input() selected?: boolean;

  @Output() selectFilm: EventEmitter<void> = new EventEmitter<void>();

  public selectFilmHandler() {
    this.selectFilm.emit();
  }
}
