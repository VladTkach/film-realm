import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {ActorDto} from "../../models/actor/actor-dto";
import {CommentDto} from "../../models/comment/comment-dto";
import {NewCommentDto} from "../../models/comment/new-comment-dto";
import {UpdateCommentDto} from "../../models/comment/update-comment-dto";

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private readonly authRoutePrefix = '/api/comment';
  constructor(private http: HttpInternalService) { }

  public getFilmComments(filmId: number) {
    return this.http.getRequest<CommentDto[]>(`${this.authRoutePrefix}/all/${filmId}`);
  }

  public addComment(newCommentDto: NewCommentDto){
    return this.http.postRequest<CommentDto>(this.authRoutePrefix, newCommentDto);
  }

  public updateComment(updateCommentDto: UpdateCommentDto){
    return this.http.putRequest<ActorDto>(this.authRoutePrefix, updateCommentDto);
  }

  public deleteComment(commentId: number){
    return this.http.deleteRequest(`${this.authRoutePrefix}/${commentId}`);
  }
}
