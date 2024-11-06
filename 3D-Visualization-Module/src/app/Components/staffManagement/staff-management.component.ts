import {Component, Inject, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {StaffService} from '../../services/StaffService/staff.service';
import {Staff} from './Staff';

@Component({
  selector: 'app-staff-management',
  templateUrl: './staff-management.component.html',
  styleUrls: ['./staff-management.component.css']
})
export class StaffManagementComponent implements OnInit {
  staffList: Staff[] = [];
  private auth: AuthService;
  private staffService: StaffService;
  showStaffList: boolean = true;
  showNameFilter: boolean = false;
  showStaffProfiles: boolean = true;
  filterName: string = '';

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

  toggleNameFilter() {
    this.showNameFilter = !this.showNameFilter;
  }

  applyNameFilter() {

    this.staffService.filterStaffProfilesByName(this.filterName).subscribe(
      (data: Staff[]) => {
        this.showStaffProfiles = true;
        console.log('Filtered staff profiles by name', this.filterName);
        this.staffList = data;
      },
      (error) => {
        this.showStaffProfiles = false;
        console.error('Error fetching staff profiles', error);
      }
    );
  }

  public addStaffProfile() {
    this.showStaffList = false;
  }

  public deleteStaffProfile(staffId: string) {

    const isConfirmed = window.confirm('Are you sure you want to proceed?');

    if (isConfirmed) {
      this.staffService.deleteStaffProfile(staffId).subscribe(
        response => {
          console.log('Staff profile deactivated:', response);

          this.staffList = this.staffList.filter(staff => staff.id !== staffId);

        },
        error => {
          console.error('Error deactivating staff profile:', error);
        }
      );
    }
  }

  public editStaffProfile(staffId: string) {
    alert('Edit staff profile with ID: ' + staffId + '\n' + 'This feature is not yet implemented');
  }

  private fetchStaffProfiles() {

    this.staffService.listStaffProfilesBySpecialization().subscribe(
      (data: Staff[]) => {
        this.staffList = data;
      },
      (error) => {
        console.error('Error fetching staff profiles', error);
      }
    );
  }
}
