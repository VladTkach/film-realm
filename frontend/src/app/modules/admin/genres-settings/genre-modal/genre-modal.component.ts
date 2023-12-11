import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {GenreService} from "../../../../core/services/genre.service";
import {NewGenreDto} from "../../../../models/genre/new-genre-dto";
import {BaseComponent} from "../../../../core/base/base.component";
import {takeUntil} from "rxjs";
import {GenreDto} from "../../../../models/genre/genre-dto";
import {UpdateGenreDto} from "../../../../models/genre/update-genre-dto";

@Component({
  selector: 'app-genre-modal',
  templateUrl: './genre-modal.component.html',
  styleUrls: ['./genre-modal.component.sass']
})
export class GenreModalComponent extends BaseComponent implements OnInit{
  @Input() isCreateMode = true;
  @Input() updatedGenre: GenreDto | undefined;

  @Output() close = new EventEmitter();
  @Output() genre = new EventEmitter<GenreDto>();
  public genreForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private genreService: GenreService) {
    super();
  }

  public closeModal() {
    this.close.emit();
  }

  public ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.genreForm = this.fb.group({
      name: [this.updatedGenre && !this.isCreateMode ? this.updatedGenre.name : '',
        [Validators.required, Validators.maxLength(50)]
      ]
    });
  }

  public create() {
    const newGenre: NewGenreDto = this.genreForm.value;

    this.genreService.addGenre(newGenre)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: genre => {
          this.genre.emit(genre);
          this.closeModal();
        }
      })
  }

  public update() {
    if (!this.updatedGenre){
      return;
    }

    const updateGenre: UpdateGenreDto = {
      id: this.updatedGenre.id,
      name: this.genreForm.value['name']
    };

    this.genreService.updateGenre(updateGenre)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: genre => {
          this.genre.emit(genre);
          this.closeModal();
        }
      });
  }
}
