import {Routes} from '@angular/router';
import {HomeComponent} from './Components/Dashboards/home/home.component';
import {AdminMenuComponent} from './Components/Dashboards/admin-home/admin-home.component';
import {OperationTypeManagementComponent} from './Components/OperationTypeManagement/operation-type-management/operation-type-management.component';
import {StaffManagementComponent} from './Components/StaffManagement/staff-management/staff-management.component';
import {AddOperationTypeComponent} from './Components/OperationTypeManagement/add-operation-type/add-operation-type.component';
import {EditOperationTypeComponent} from './Components/OperationTypeManagement/edit-operation-type/edit-operation-type.component';
import {ViewOperationTypeComponent} from './Components/OperationTypeManagement/view-operation-type/view-operation-type.component';
import {EditStaffProfileComponent} from './Components/StaffManagement/edit-staff-profile/edit-staff-profile.component';
import {AddStaffProfileComponent} from './Components/StaffManagement/add-staff-profile/add-staff-profile.component';
import {RegisterPatientComponent} from './Components/Patient/register-patient/register-patient.component';
import {
  RegisterPatientProfileComponent
} from './Components/PatientManagement/register-patient-profile/register-patient-profile.component';

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
    path: 'admin/patient',
    component: RegisterPatientProfileComponent,
    title: 'PatientProfileManagement',
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
  },
  {
    path: 'admin/staff/edit',
    component: EditStaffProfileComponent,
    title: 'EditStaffProfile',
  },
  {
    path: 'admin/staff/add',
    component: AddStaffProfileComponent,
    title: 'AddStaffProfile',
  },
  {
    path: 'admin/patient/register',
    component: RegisterPatientProfileComponent,
    title: 'RegisterPatientProfile',
  },

];
