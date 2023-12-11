import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {takeUntil} from "rxjs";
import {BaseComponent} from "../../../../core/base/base.component";
import {ActorDto} from "../../../../models/actor/actor-dto";
import {ActorService} from "../../../../core/services/actor.service";
import {NewActorDto} from "../../../../models/actor/new-actor-dto";
import {UpdateActorDto} from "../../../../models/actor/update-actor-dto";

@Component({
  selector: 'app-actor-modal',
  templateUrl: './actor-modal.component.html',
  styleUrls: ['./actor-modal.component.sass']
})
export class ActorModalComponent extends BaseComponent implements OnInit {
  @Input() isCreateMode = true;
  @Input() updatedActor: ActorDto | undefined;

  @Output() close = new EventEmitter();
  @Output() actor = new EventEmitter<ActorDto>();
  public actorForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder, private actorService: ActorService) {
    super();
  }

  public closeModal() {
    this.close.emit();
  }

  public ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.actorForm = this.fb.group({
      name: [this.updatedActor && this.isCreateMode ? this.updatedActor.name : '',
        [Validators.required, Validators.maxLength(50)]
      ]
    });
  }

  public create() {
    const newActor: NewActorDto = this.actorForm.value;

    this.actorService.addActor(newActor)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: actor => {
          this.actor.emit(actor);
          this.closeModal();
        }
      })
  }

  public update() {
    if (!this.updatedActor) {
      return;
    }

    const updateActor: UpdateActorDto = {
      id: this.updatedActor.id,
      name: this.actorForm.value['name']
    };

    this.actorService.updateActor(updateActor)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: actor => {
          this.actor.emit(actor);
          this.closeModal();
        }
      });
  }
}
