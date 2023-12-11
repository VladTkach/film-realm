import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BaseComponent} from "../../../core/base/base.component";
import {FilmDto} from "../../../models/film/film-dto";
import {FilmService} from "../../../core/services/film.service";
import {takeUntil} from "rxjs";
import {CommentDto} from "../../../models/comment/comment-dto";
import {CommentService} from "../../../core/services/comment.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {NewCommentDto} from "../../../models/comment/new-comment-dto";
import {AuthService} from "../../../core/services/auth.service";

@Component({
  selector: 'app-film-page',
  templateUrl: './film-page.component.html',
  styleUrls: ['./film-page.component.sass']
})
export class FilmPageComponent extends BaseComponent implements OnInit {
  public film?: FilmDto;
  public comments: CommentDto[] = [];
  public commentForm: FormGroup = new FormGroup({});

  constructor(private route: ActivatedRoute, private filmService: FilmService,
              private commentService: CommentService, private fb: FormBuilder, private authService: AuthService) {
    super();
  }

  ngOnInit() {
    this.getFilm();
    this.initializeForm();
  }

  public getGenresString() {
    return this.film?.genres.map(g => g.name).join(', ');
  }

  public getActorsString() {
    return this.film?.actors.map(g => g.name).join(', ');
  }

  public addComment() {
    this.authService.currentUser$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: userDto => {
          if (userDto){
            this.sendComment(userDto.id)
          }
        }
      });
  }

  private sendComment(id: number) {
    if (!this.film){
      return;
    }

    const newComment: NewCommentDto = {
      text: this.commentForm.value['newComment'],
      filmId: this.film.id,
      userId: id
    };

    this.commentService.addComment(newComment)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: comment => {
          this.comments.push(comment);
        }
      });
  }
  private getFilm() {
    const filmId = this.route.snapshot.paramMap.get('id');
    if (filmId === null) {
      return;
    }

    this.filmService.getFilmById(+filmId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: film => {
          this.film = film;
          this.getComments();
        }
      });
  }

  private getComments() {
    if (!this.film){
      return;
    }

    this.commentService.getFilmComments(this.film.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: comments => {
          this.comments = comments;
        }
      })
  }

  private initializeForm() {
    this.commentForm = this.fb.group({
      newComment: ['']
    })
  }


}
