import {Component, EventEmitter, Input, Output} from '@angular/core';
import {UserDto} from "../../../../models/user/user-dto";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.sass']
})
export class UserCardComponent {
  @Input() user?: UserDto;
  @Input() selected?: boolean;

  @Output() selectUser: EventEmitter<void> = new EventEmitter<void>();

  public selectUserHandler() {
    this.selectUser.emit();
  }
}
