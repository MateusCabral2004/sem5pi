import {Routes} from '@angular/router';
import {HomeComponent} from './Components/home/home.component';
import {AdminMenuComponent} from './Components/admin-home/admin-home.component';
import {OperationTypeManagementComponent} from './Components/operation-type-management/operation-type-management.component';
import {StaffManagementComponent} from './Components/staffManagement/staff-management.component';
import {RegisterPatientComponent} from './Components/Patient/register-patient/register-patient.component';
import {AddOperationTypeComponent} from './Components/add-operation-type/add-operation-type.component';
import {EditOperationTypeComponent} from './Components/edit-operation-type/edit-operation-type.component';
import {ViewOperationTypeComponent} from './Components/view-operation-type/view-operation-type.component';

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
    runGuardsAndResolvers: 'always',
    title: 'OperationTypeManagement',
  },
  {
    path: 'admin/staff',
    component: StaffManagementComponent,
    title: 'StaffManagement',
  },
  {
    path: 'admin/operationTypeManagement/add',
    component: AddOperationTypeComponent,
    title: 'AddOperationType',
  },
  {
    path: 'admin/staffManagement',
    component: StaffManagementComponent,
    title: 'StaffManagement',
  },
  {
    path: 'unregistered',
    component: RegisterPatientComponent,
    title: 'registerPatient',
  },
  {
    path: 'patient',
    component: RegisterPatientComponent,
    title: 'Patient Manegement',
  },
   {
    path: 'admin/operationTypeManagement/edit',
    component: EditOperationTypeComponent,
    title: 'EditOperationType',
  },
  {
    path: 'admin/operationTypeManagement/view',
    component: ViewOperationTypeComponent,
    title: 'ViewOperationType',
  }

];
