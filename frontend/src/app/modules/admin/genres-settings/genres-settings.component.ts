import {Component, OnInit} from '@angular/core';
import {GenreDto} from "../../../models/genre/genre-dto";
import {BaseComponent} from "../../../core/base/base.component";
import {GenreService} from "../../../core/services/genre.service";
import {takeUntil} from "rxjs";

@Component({
  selector: 'app-genres-settings',
  templateUrl: './genres-settings.component.html',
  styleUrls: ['./genres-settings.component.sass']
})
export class GenresSettingsComponent extends BaseComponent implements OnInit {
  public isGenreModalOpen = false;
  public isCreateMode = true;
  public genres: GenreDto[] = [];
  public selectedGenre?: GenreDto;

  constructor(private genreService: GenreService) {
    super();
  }

  ngOnInit() {
    this.genreService.getAllGenres()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: genres => {
          this.genres = genres;
        }
      })
  }

  public openGenreModal(isCreateMode: boolean) {
    this.isCreateMode = isCreateMode;
    this.isGenreModalOpen = true;
  }

  public closeGenreModal() {
    this.isGenreModalOpen = false;
  }

  public genreReceive(genre: GenreDto) {
    if (this.isCreateMode) {
      this.genres?.push(genre);
    } else {
      this.updateGenre(genre);
    }
  }

  public isSelected(genre: GenreDto) {
    return this.selectedGenre === genre;
  }

  public toggleSelection(genre: GenreDto) {
    if (this.selectedGenre === genre) {
      this.selectedGenre = undefined;
    } else {
      this.selectedGenre = genre;
    }
  }

  public deleteGenre() {
    if (!this.selectedGenre) {
      return;
    }

    this.genreService.deleteGenre(this.selectedGenre.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: () => {
          const genreIndex = this.genres.findIndex(g => g.id === this.selectedGenre?.id);
          if (genreIndex != -1) {
            this.genres.splice(genreIndex, 1);
            this.selectedGenre = undefined;
          }
        }
      })
  }

  private updateGenre(genre: GenreDto) {
    const genreIndex = this.genres.findIndex(g => g.id === genre.id);
    if (genreIndex != -1) {
      this.genres.splice(genreIndex, 1, genre);
    }
  }
}
