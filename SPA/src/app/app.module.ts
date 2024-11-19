import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {AppComponent} from './app.component';
import {CommonModule} from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {FormsModule} from '@angular/forms';
import {routes} from './app.routes';

import {HomeComponent} from './Components/Dashboards/home/home.component';
import {AdminMenuComponent} from './Components/Dashboards/admin-home/admin-home.component';
import {MenuBoxComponent} from './Components/Shared/menu-box/menu-box.component';
import {MenuComponent} from './Components/Shared/menu/menu.component';
import {ProfilePictureMenuComponent} from './Components/Shared/profile-picture-menu/profile-picture-menu.component';
import {OperationTypeManagementComponent} from './Components/OperationTypeManagement/operation-type-management/operation-type-management.component';
import {StaffManagementComponent} from './Components/StaffManagement/staff-management/staff-management.component';
import {BaseDashboardComponent} from './Components/Dashboards/base-dashboard/base-dashboard.component';
import {HomeButtonComponent} from './Components/Shared/home-button/home-button.component';
import {AddOperationTypeComponent} from './Components/OperationTypeManagement/add-operation-type/add-operation-type.component';
import {EditOperationTypeComponent} from './Components/OperationTypeManagement/edit-operation-type/edit-operation-type.component';
import {GoBackButtonComponent} from './Components/Shared/go-back-button/go-back-button.component';
import {DeleteStaffProfileButtonComponent} from './Components/StaffManagement/delete-staff-profile-button/delete-staff-profile-button.component';
import {EditStaffProfileButtonComponent} from './Components/StaffManagement/edit-staff-profile-button/edit-staff-profile-button.component';
import {StaffDetailsComponent} from './Components/StaffManagement/staff-details/staff-details.component';
import {StaffProfileListComponent} from './Components/StaffManagement/staff-profile-list/staff-profile-list.component';
import {FilterButtonComponent} from './Components/Shared/filter-button/filter-button.component';
import {ConfirmModalComponent} from './Components/Shared/confirm-modal/confirm-modal.component';
import {EnterFilterNameComponent} from './Components/Shared/enter-filter-name/enter-filter-name.component';
import {ViewOperationTypeComponent} from './Components/OperationTypeManagement/view-operation-type/view-operation-type.component';
import {EditStaffProfileComponent} from './Components/StaffManagement/edit-staff-profile/edit-staff-profile.component';
import {AddStaffProfileComponent} from './Components/StaffManagement/add-staff-profile/add-staff-profile.component';
import {BackgroudCardComponent} from './Components/Shared/backgroud-card/backgroud-card.component';
import {SafeUrlPipe} from './Components/LinkTo3dModule/safeUrlPipe/safe-url.pipe';
import {Connect3dComponent} from './Components/LinkTo3dModule/connect3d/connect3d.component';
import {RegisterPatientProfileComponent} from './Components/PatientManagement/register-patient-profile/register-patient-profile.component';
import {RegisterPatientComponent} from './Components/Patient/register-patient/register-patient.component';
import {PatientManagementComponent} from './Components/PatientManagement/patientManagement/patient-management.component';
import {PatientProfileListComponent} from './Components/PatientManagement/patient-profile-list/patient-profile-list.component';
import {PatientProfileDetailsComponent} from './Components/PatientManagement/patient-profile-details/patient-profile-details.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminMenuComponent,
    MenuBoxComponent,
    MenuComponent,
    ProfilePictureMenuComponent,
    OperationTypeManagementComponent,
    StaffManagementComponent,
    BaseDashboardComponent,
    HomeButtonComponent,
    RegisterPatientComponent,
    AddOperationTypeComponent,
    EditOperationTypeComponent,
    GoBackButtonComponent,
    DeleteStaffProfileButtonComponent,
    EditStaffProfileButtonComponent,
    StaffDetailsComponent,
    StaffProfileListComponent,
    FilterButtonComponent,
    ConfirmModalComponent,
    EnterFilterNameComponent,
    ViewOperationTypeComponent,
    EditStaffProfileComponent,
    AddStaffProfileComponent,
    BackgroudCardComponent,
    Connect3dComponent,
    SafeUrlPipe
    RegisterPatientProfileComponent,
    PatientManagementComponent,
    PatientProfileListComponent,
    PatientProfileDetailsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
