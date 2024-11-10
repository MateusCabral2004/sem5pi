import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {MenuComponent} from './Components/menu/menu.component';
import {AppComponent} from './app.component';
import {HomeComponent} from './Components/home/home.component';
import {AdminMenuComponent} from './Components/admin-home/admin-home.component';
import {MenuBoxComponent} from './Components/menu-box/menu-box.component';
import {ProfilePictureMenuComponent} from './Components/profile-picture-menu/profile-picture-menu.component';
import {OperationTypeManagementComponent} from './Components/operation-type-management/operation-type-management.component';
import {BaseDashboardComponent} from './Components/base-dashboard/base-dashboard.component';
import {HomeButtonComponent} from './Components/home-button/home-button.component';
import {AddOperationTypeComponent} from './Components/add-operation-type/add-operation-type.component';
import {StaffManagementComponent} from './Components/staffManagement/staff-management.component';

import {RegisterPatientComponent} from './Components/Patient/register-patient/register-patient.component';

import {routes} from './app.routes';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import {DeleteStaffProfileButtonComponent} from './Components/delete-staff-profile-button/delete-staff-profile-button.component';
import {EditStaffProfileButtonComponent} from './Components/edit-staff-profile-button/edit-staff-profile-button.component';
import {StaffDetailsComponent} from './Components/staff-details/staff-details.component';
import {StaffProfileListComponent} from './Components/staff-profile-list/staff-profile-list.component';
import {FilterStaffButtonComponent} from './Components/filter-staff-button/filter-staff-button.component';
import {EditOperationTypeComponent} from './Components/edit-operation-type/edit-operation-type.component';
import {GoBackButtonComponent} from './Components/go-back-button/go-back-button.component';
import {ConfirmModalComponent} from './Components/confirm-modal/confirm-modal.component';
import {BackgroudCardComponent} from './Components/backgroud-card/backgroud-card.component';
import {EnterFilterNameComponent} from './Components/enter-filter-name/enter-filter-name.component';

import {routes} from './app.routes';
import {ListOperationRequestComponent} from './Components/list-operation-request/list-operation-request.component';
import {FormsModule} from '@angular/forms';


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
    FilterStaffButtonComponent,
    ConfirmModalComponent,
    BackgroudCardComponent,
    ListOperationRequestComponent,
    EnterFilterNameComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // Add HttpClientModule here
    RouterModule.forRoot(routes),
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
