import {Component, OnInit} from '@angular/core';
import {GenreDto} from "../../../models/genre/genre-dto";
import {GenreService} from "../../../core/services/genre.service";
import {takeUntil} from "rxjs";
import {BaseComponent} from "../../../core/base/base.component";
import {ActorDto} from "../../../models/actor/actor-dto";
import {ActorService} from "../../../core/services/actor.service";

@Component({
  selector: 'app-actors-settings',
  templateUrl: './actors-settings.component.html',
  styleUrls: ['./actors-settings.component.sass']
})
export class ActorsSettingsComponent extends BaseComponent implements OnInit{
  public isActorModalOpen = false;
  public isCreateMode = true;
  public actors: ActorDto[] = [];
  public selectedActor?: ActorDto;

  constructor(private actorService: ActorService) {
    super();
  }

  ngOnInit() {
    this.actorService.getAllActors()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: actors => {
          this.actors = actors;
        }
      })
  }

  public openActorModal(isCreateMode: boolean) {
    this.isCreateMode = isCreateMode;
    this.isActorModalOpen = true;
  }

  public closeActorModal() {
    this.isActorModalOpen = false;
  }

  public actorReceive(actor: ActorDto) {
    if (this.isCreateMode) {
      this.actors?.push(actor);
    } else {
      this.updateActor(actor);
    }
  }

  public isSelected(actor: ActorDto) {
    return this.selectedActor === actor;
  }

  public toggleSelection(actor: ActorDto) {
    if (this.selectedActor === actor) {
      this.selectedActor = undefined;
    } else {
      this.selectedActor = actor;
    }
  }

  public deleteActor() {
    if (!this.selectedActor) {
      return;
    }

    this.actorService.deleteActor(this.selectedActor.id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: () => {
          const actorIndex = this.actors.findIndex(g => g.id === this.selectedActor?.id);
          if (actorIndex != -1) {
            this.actors.splice(actorIndex, 1);
            this.selectedActor = undefined;
          }
        }
      })
  }

  private updateActor(actor: ActorDto) {
    const actorIndex = this.actors.findIndex(g => g.id === actor.id);
    if (actorIndex != -1) {
      this.actors.splice(actorIndex, 1, actor);
    }
  }
}
