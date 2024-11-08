import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../services/AuthService/auth.service';

import {Router} from '@angular/router';
import {StaffService} from '../../services/StaffService/staff.service';
import {Staff} from './Staff';
import {ConfirmModalComponent} from '../confirm-modal/confirm-modal.component';
import {EnterFilterNameComponent} from '../enter-filter-name/enter-filter-name.component';

@Component({
  selector: 'app-staff-management',
  templateUrl: './staff-management.component.html',
  styleUrls: ['./staff-management.component.css']
})
export class StaffManagementComponent implements OnInit {
  public staffList: Staff[] = [];
  private auth: AuthService;
  private staffService: StaffService;
  private staffId: string = '';
  public showStaffList: boolean = true;
  public showNoStaffsFound: boolean = false;
  public showResetFilterButton: boolean = false;

  @ViewChild(ConfirmModalComponent) confirmModal!: ConfirmModalComponent;
  @ViewChild(EnterFilterNameComponent) enterFilterName!: EnterFilterNameComponent;

  constructor(@Inject(AuthService) auth: AuthService, @Inject(StaffService) staffService: StaffService, private router: Router) {
    this.auth = auth;
    this.staffService = staffService;
    this.validateUserRole();
  }

  ngOnInit(): void {
    this.fetchStaffProfiles();
  }

  private validateUserRole() {
    const expectedRole = "Admin";
    this.auth.validateUserRole(expectedRole);
  }

  applyNameFilter(filterName: string) {

    this.staffService.filterStaffProfilesByName(filterName).subscribe(
      (data: Staff[]) => {
        this.showNoStaffsFound = false;
        this.showStaffList = true;
        this.staffList = data;
        this.showResetFilterButton = true;
      },
      (error) => {

        if (error.status === 404) {

          this.staffList = [];
          this.showStaffList = false;
          this.showNoStaffsFound = true;
          this.showResetFilterButton = true;
          console.log(error);

        } else {

          this.showStaffList = false;
          console.error('Error fetching staff profiles', error);

        }
      }
    );
  }

  public addStaffProfile() {
    this.showStaffList = false;
  }

  public resetFilter() {
    this.fetchStaffProfiles();
    this.showNoStaffsFound = false;
    this.showResetFilterButton = false;
  }

  public confirmDeleteStaffProfile(staffId: string) {

    this.staffId = staffId;
    this.confirmModal.open("Are you sure you want to proceed?");
  }

  public handleSelectedFilter(filter: string) {

    if(filter === 'Name') {
      this.enterFilterName.open();
    }

    if(filter === 'Email') {
      alert('Filter by Email\nThis feature is not yet implemented');
    }

    if(filter === 'Specialization') {
      alert('Filter by Specialization\nThis feature is not yet implemented');
    }

  }

  public handleDeleteStaffProfileConfirmation(isConfirmed: boolean) {

    if (isConfirmed) {
      this.staffService.deleteStaffProfile(this.staffId).subscribe(
        response => {
          console.log('Staff profile deactivated:', response);

          this.staffList = this.staffList.filter(staff => staff.id !== this.staffId);

        },
        error => {

          if (error.status === 404) {

            console.log(error);
          } else {
            console.error('Error deactivating staff profile:', error);
          }
        }
      );
    }
  }

  public editStaffProfile(staffId: string) {
    alert('Edit staff profile with ID: ' + staffId + '\n' + 'This feature is not yet implemented');
  }

  private fetchStaffProfiles() {

    this.staffService.listAllStaffProfiles().subscribe(
      (data: Staff[]) => {
        this.staffList = data;
      },
      (error) => {
        console.error('Error fetching staff profiles', error);
      }
    );
  }
}
