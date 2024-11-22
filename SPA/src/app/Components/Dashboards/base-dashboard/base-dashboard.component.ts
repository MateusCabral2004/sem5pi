import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-base-dashboard',
  templateUrl: 'base-dashboard.component.html',
  styleUrls: ['base-dashboard.component.css'],
  standalone: false
})
export class BaseDashboardComponent {
  @Input() title: string;
  @Input() menuItems: any[] = [];

  constructor() {
    this.title = '';
  }

}
