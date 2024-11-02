import { Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { AdminMenuComponent } from './Components/admin-home/admin-home.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home',
  },
  {
    path: 'admin',
    component: AdminMenuComponent,
    title: 'AdminHome',
  }
];
