import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-menu-box',
  templateUrl: './menu-box.component.html',
  styleUrls: ['./menu-box.component.css'],
  standalone: false
})
export class MenuBoxComponent {
  @Input() title: string = '';
  @Input() icon: string = '';
  @Input() link: string = '';
}
