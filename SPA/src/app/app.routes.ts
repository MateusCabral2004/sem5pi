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
import {Connect3dComponent} from './Components/LinkTo3dModule/connect3d/connect3d.component';
import {RegisterPatientComponent} from './Components/Patient/register-patient/register-patient.component';
import {RegisterPatientProfileComponent} from './Components/PatientManagement/register-patient-profile/register-patient-profile.component';
import {PatientManagementComponent} from './Components/PatientManagement/patientManagement/patient-management.component';
import {AuthGuard} from './Guard/auth.guard';


export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    title: 'Home',
  },
  {
    path: 'admin',
    component: AdminMenuComponent,
    canActivate: [AuthGuard],
    data: {roles: ['admin']},
    title: 'AdminHome',
  },
  {
    path: 'admin/operationTypeManagement',
    component: OperationTypeManagementComponent,
    canActivate: [AuthGuard],
    data: {roles: ['admin']},
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
    component: PatientManagementComponent,
    title: 'PatientProfileManagement',
  },
  {
    path: 'admin/operationTypeManagement/add',
    canActivate: [AuthGuard],
    data: {roles: ['admin']},
    component: AddOperationTypeComponent,
    title: 'AddOperationType',
  },
  {
    path: 'admin/patient/register',
    component: RegisterPatientProfileComponent,
    title: 'RegisterPatientProfileComponent',
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
     canActivate: [AuthGuard],
     data: {roles: ['admin']},
    title: 'EditOperationType',
  },
  {
    path: 'admin/operationTypeManagement/view',
    component: ViewOperationTypeComponent,
    canActivate: [AuthGuard],
    data: {roles: ['admin']},
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
    path: '3d',
    component: Connect3dComponent,
    title: '3D',
  },
  {
    path: 'admin/patient/register',
    component: RegisterPatientProfileComponent,
    title: 'RegisterPatientProfile',
  }
];
