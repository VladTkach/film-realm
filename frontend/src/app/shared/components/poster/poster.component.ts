import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-poster',
  templateUrl: './poster.component.html',
  styleUrls: ['./poster.component.sass']
})
export class PosterComponent {
  @Input() public photoUrl : string | ArrayBuffer | null = null;
  @Input() public width: string | number = 60;
  @Input() public height: string | number = 100;
}
