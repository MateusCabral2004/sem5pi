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
import {

  ListOperationRequestComponent
} from './Components/OperationRequest/list-operation-request/list-operation-request.component';
import {
  DeletePatientAccoutComponent
} from './Components/Patient/delete-patient-accout1/delete-patient-accout.component';
import {
  EditPatientProfileComponent
} from './Components/PatientManagement/edit-patient-profile/edit-patient-profile.component';
import {DoctorMenuComponent} from './Components/Dashboards/doctor-home/doctor-home.component';
import {
  OperationRequestManagementComponent
} from './Components/OperationRequestManagement/operation-request-management/operation-request-management.component';
import {
  AddOperationRequestComponent
} from './Components/OperationRequestManagement/add-operation-request/add-operation-request.component';



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
    path: 'doctor',
    component: DoctorMenuComponent,
    canActivate: [AuthGuard],
    data: {roles: ['doctor']},
    title: 'DoctorHome',
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
    path: 'doctor/operationRequestManagement',
    component: OperationRequestManagementComponent,
    canActivate: [AuthGuard],
    data: {roles: ['doctor']},
    runGuardsAndResolvers: 'always',
    title: 'OperationRequestManagement',
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
    path: 'doctor/operationRequestManagement/add',
    canActivate: [AuthGuard],
    data: {roles: ['doctor']},
    component: AddOperationRequestComponent,
    title: 'AddOperationRequest',
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

    path: 'doctor',
    component: ListOperationRequestComponent,
    title: 'doctor operations list',
  },
  {
    path: 'deleteAccount',
    component: DeletePatientAccoutComponent,
    title: 'Delete Patient Account',
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
  },
  {
    path: 'admin/patient/edit',
    component: EditPatientProfileComponent,
    title: 'EditPatientProfileComponent',
  }
];
