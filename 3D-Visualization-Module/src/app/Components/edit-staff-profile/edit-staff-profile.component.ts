import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {Staff} from '../../Domain/Staff';
import {StaffService} from '../../services/StaffService/staff.service';


@Component({
  selector: 'app-edit-staff-profile',
  templateUrl: './edit-staff-profile.component.html',
  styleUrl: './edit-staff-profile.component.css'
})
export class EditStaffProfileComponent implements OnInit {

  public updatedStaffProfile!: Staff;
  public typedStaffProfile!: Staff;
  public originalStaffProfile!: Staff;
  public showConfirmationPopup: boolean = false;
  public errorMessage: string = '';
  public showErrorMessagePopup: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private staffService: StaffService,
  ) {
  }

  ngOnInit() {
    const staffFromState = history.state.staff;
    if (staffFromState) {
      this.typedStaffProfile = {...staffFromState};
      this.originalStaffProfile = {...staffFromState};
    } else {
      console.error('Operation data not found in router state.');
      alert('Error loading operation details.');
      this.router.navigate(['/admin/staff']);
    }
  }

  public isAtLeastOneFieldFilled(): boolean {

    let isAtLeastOneFieldFilled = false;

    if (!this.updatedStaffProfile) {
      this.updatedStaffProfile = {} as Staff;
      this.updatedStaffProfile.id = this.typedStaffProfile.id;
    }

    if (this.checkForChangesInEmail()) {
      isAtLeastOneFieldFilled = true;
      this.updatedStaffProfile.email = this.typedStaffProfile.email.trim();
    }

    if (this.checkForChangesInPhoneNumber()) {
      isAtLeastOneFieldFilled = true;
      this.updatedStaffProfile.phoneNumber = this.typedStaffProfile.phoneNumber;
    }

    if (this.checkForChangesInSpecialization()) {
      isAtLeastOneFieldFilled = true;
      this.updatedStaffProfile.specialization = this.typedStaffProfile.specialization.trim();
    }


    return isAtLeastOneFieldFilled;
  }

  public saveChanges() {
    this.staffService.updateStaffProfile(this.updatedStaffProfile).subscribe(
      () => {

        if (!this.detectContactInfoChanges()) {
          this.router.navigate(['/admin/staff']).then(() => {
            window.location.reload();
          });
        } else {
          this.showConfirmationPopup = true;
        }
      },
      (error) => {
        if (error.status === 600) {

          this.errorMessage = error.error;
          this.showErrorMessagePopup = true;

        } else if (error.status === 601) {

          this.errorMessage = error.error;
          this.showErrorMessagePopup = true;

        } else if (error.status === 602) {

          this.errorMessage = error.error;
          this.showErrorMessagePopup = true;

        } else if (error.status === 603) {

          this.errorMessage = error.error;
          this.showErrorMessagePopup = true;

        } else {

          console.log('Error updating staff profile:', error);

        }
      }
    );
  }


  private checkForChangesInEmail(): boolean {
    return this.typedStaffProfile.email !== this.originalStaffProfile.email;
  }

  private checkForChangesInPhoneNumber(): boolean {
    return this.typedStaffProfile.phoneNumber !== this.originalStaffProfile.phoneNumber;
  }

  private checkForChangesInSpecialization(): boolean {
    return this.typedStaffProfile.specialization !== this.originalStaffProfile.specialization;
  }

  public detectContactInfoChanges(): boolean {
    return this.checkForChangesInEmail() || this.checkForChangesInPhoneNumber();
  }

  public closeConfirmationPopupAndRedirect(): void {
    this.showConfirmationPopup = false;
    this.router.navigate(['/admin/staff']);
  }

  public closeErrorMessagePopup(): void {
    this.showErrorMessagePopup = false;
  }

}
