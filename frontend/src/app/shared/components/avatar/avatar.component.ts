import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.sass']
})
export class AvatarComponent implements OnChanges{
  @Input() public photoUrl : string | undefined;
  @Input() public name : string | undefined;
  @Input() public size: string | number = 50;

  public showInitials = false;
  public initials = '';
  public circleColor = '';
  public color = ['#EB7181', '#468547', '#FFD558' ,'#3670B2'];

  ngOnChanges(changes: SimpleChanges) {
    console.log(changes);
    if (changes['photoUrl']) {
      console.log(this.photoUrl)
      if (!this.photoUrl){
        console.log('show initials')
        this.showInitials = true;
        this.createInitials();

        const randomIndex = Math.floor(Math.random()) * Math.floor(this.color.length);
        this.circleColor = this.color[randomIndex];
      }
      else {
        this.showInitials = false;
      }
    }
  }

  public getFontSize(): number {
    // Розмір шрифту буде 40% від переданого розміру, наприклад
    return Number(this.size) * 0.4;
  }

  private createInitials(){
    if (!this.name){
      return;
    }
    const words = this.name.split(' ').filter(word => word); // Розділити рядок на слова і видалити порожні
    console.log(words);
    let initials = '';
    const maxInitials = 2;

    for (const word of words) {
        initials += word[0];

        if (initials.length === maxInitials) {
          break;
      }
    }
    console.log(initials);
    this.initials = initials;
  }
}
