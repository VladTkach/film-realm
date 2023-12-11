import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {ActorDto} from "../../models/actor/actor-dto";
import {NewActorDto} from "../../models/actor/new-actor-dto";
import {UpdateActorDto} from "../../models/actor/update-actor-dto";

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  private readonly authRoutePrefix = '/api/actor';
  constructor(private http: HttpInternalService) { }

  public getAllActors() {
    return this.http.getRequest<ActorDto[]>(`${this.authRoutePrefix}/all`);
  }

  public addActor(newActorDto: NewActorDto){
    return this.http.postRequest<ActorDto>(this.authRoutePrefix, newActorDto);
  }

  public updateActor(updateActorDto: UpdateActorDto){
    return this.http.putRequest<ActorDto>(this.authRoutePrefix, updateActorDto);
  }

  public deleteActor(actorId: number){
    return this.http.deleteRequest(`${this.authRoutePrefix}/${actorId}`);
  }
}
