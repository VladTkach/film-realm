import {Component, EventEmitter, Input, Output} from '@angular/core';
import {GenreDto} from "../../../../models/genre/genre-dto";

@Component({
  selector: 'app-genre-card',
  templateUrl: './genre-card.component.html',
  styleUrls: ['./genre-card.component.sass']
})
export class GenreCardComponent {
  @Input() genre?: GenreDto;
  @Input() selected?: boolean;

  @Output() selectGenre: EventEmitter<void> = new EventEmitter<void>();

  public selectGenreHandler() {
    this.selectGenre.emit();
  }
}
