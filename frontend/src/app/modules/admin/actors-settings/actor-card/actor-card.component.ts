import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ActorDto} from "../../../../models/actor/actor-dto";

@Component({
  selector: 'app-actor-card',
  templateUrl: './actor-card.component.html',
  styleUrls: ['./actor-card.component.sass']
})
export class ActorCardComponent {
  @Input() actor?: ActorDto;
  @Input() selected?: boolean;

  @Output() selectActor: EventEmitter<void> = new EventEmitter<void>();

  public selectActorHandler() {
    this.selectActor.emit();
  }
}
