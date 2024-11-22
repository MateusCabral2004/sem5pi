import { Component, Input } from '@angular/core';

interface MenuItem {
  title: string;
  icon: string;
  link: string;
}

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
  standalone: false
})
export class MenuComponent {
  @Input() menuItems: MenuItem[] = [];
}
