import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../services/AuthService/auth.service';

import {Router} from '@angular/router';
import {StaffService} from '../../services/StaffService/staff.service';
import {Staff} from '../../Domain/Staff';
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
  public filterByName: boolean = false;
  public currentFilter: string = '';
  public showNoSpecializationFound: boolean = false;
  public showFilterStaffButton: boolean = true;
  public showInvalidEmailFormat: boolean = false;

  public showEmptyNameError: boolean = false;
  public showEmptyEmailError: boolean = false;
  public showEmptySpecializationError: boolean = false;

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

  public applyNameFilter(filterName: string) {

    const trimmedFilterName = filterName.trim();

    this.staffService.filterStaffProfilesByName(trimmedFilterName).subscribe(

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

        } else {

          this.showFilterStaffButton = true;
          this.showStaffList = false;
          this.showEmptyNameError = true;
        }
      }
    );
  }

  public applyEmailFilter(filterEmail: string) {

    const trimmedFilterEmail = filterEmail.trim();

    this.staffService.filterStaffProfilesByEmail(trimmedFilterEmail).subscribe(

      (data: Staff) => {
        this.showNoStaffsFound = false;
        this.showStaffList = true;
        this.staffList = [];
        this.staffList.push(data);
        this.showResetFilterButton = true;
      },
      (error) => {

        if (error.status === 404) {

          this.staffList = [];
          this.showStaffList = false;
          this.showNoStaffsFound = true;
          this.showResetFilterButton = true;
        }
        else {

          this.showFilterStaffButton = true;
          this.showStaffList = false;
          this.showInvalidEmailFormat = true;
          this.showEmptyEmailError = true;

        }
      }
    );
  }

  public applySpecializationFilter(filterSpecialization: string) {

    const trimmedFilterSpecialization = filterSpecialization.trim();

    this.staffService.filterStaffProfilesBySpecialization(trimmedFilterSpecialization).subscribe(
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

        } else if (error.status === 403) {


          console.log('Error:', error);

          this.staffList = [];
          this.showStaffList = false;
          this.showNoStaffsFound = false;
          this.showNoSpecializationFound = true;
          this.showResetFilterButton = true;

        }else {

          console.log('Error2:', error);

          this.showFilterStaffButton = true;
          this.showStaffList = false;
          this.showEmptySpecializationError = true;

        }
      }
    );
  }

  public addStaffProfile() {
    this.router.navigate(['admin/staff/add']);
  }

  public resetFilter() {
    this.staffList = [];
    this.showFilterStaffButton = true;
    this.showStaffList = true;
    this.fetchStaffProfiles();
    this.showNoStaffsFound = false;
    this.showResetFilterButton = false;
    this.showNoSpecializationFound = false;
  }

  public confirmDeleteStaffProfile(staffId: string) {

    this.staffId = staffId;
    this.confirmModal.open("Are you sure you want to proceed?");
  }

  public handleFilterSelection(filterValue: string) {

    this.showFilterStaffButton = false;
    this.showEmptyNameError = false;
    this.showEmptyEmailError = false;
    this.showEmptySpecializationError = false;
    this.showInvalidEmailFormat = false;
    this.showNoSpecializationFound = false;


    if (this.currentFilter === 'name') {
      this.applyNameFilter(filterValue);
    } else if (this.currentFilter === 'email') {
      this.applyEmailFilter(filterValue);
    } else if (this.currentFilter === 'specialization') {
      this.applySpecializationFilter(filterValue);
    }
  }

  public handleSelectedFilter(filter: string) {

    if(filter === 'Name') {
      this.filterByName = true;
      this.enterFilterName.open("name");
    }

    if(filter === 'Email') {
      this.enterFilterName.open("email");
    }

    if(filter === 'Specialization') {
      this.enterFilterName.open("specialization");
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

  public editStaffProfile(staff: Staff) {
    this.router.navigate(['admin/staff/edit'], { state: { staff: staff } });
  }

  public fetchStaffProfiles() {

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
