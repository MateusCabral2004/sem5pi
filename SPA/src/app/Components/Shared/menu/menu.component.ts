import { Component, Input } from '@angular/core';

interface MenuItem {
  title: string;
  icon: string;
  link: string;
}

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  @Input() menuItems: MenuItem[] = [];
}
