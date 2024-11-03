import { Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { AdminMenuComponent } from './Components/admin-home/admin-home.component';
import {
  OperationTypeManagementComponent
} from './Components/operation-type-management/operation-type-management.component';
import {StaffManagementComponent} from './Components/staffManagement/staff-management.component';

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
  },
  {
    path: 'admin/operationTypeManagement',
    component: OperationTypeManagementComponent,
    title: 'OperationTypeManagement',
  },
  {
    path: 'admin/staffManagement',
    component: StaffManagementComponent,
    title: 'StaffManagement',
  }
];
