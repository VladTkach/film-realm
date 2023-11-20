import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-base-modal',
  templateUrl: './base-modal.component.html',
  styleUrls: ['./base-modal.component.sass']
})
export class BaseModalComponent {
  @Output() close = new EventEmitter();

  public closeModal(){
    this.close.emit();
  }
}
