import {Component, OnInit} from '@angular/core';
import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';
import {ActivatedRoute, Router} from '@angular/router';
import {PatientProfile} from '../../../Domain/PatientProfile';

@Component({
  selector: 'app-edit-patient-profile',
  templateUrl: './edit-patient-profile.component.html',
  styleUrl: './edit-patient-profile.component.css'
})
export class EditPatientProfileComponent implements OnInit {

  public updatedPatientProfile!: PatientProfile;
  public typedPatientProfile!: PatientProfile;
  public originalPatientProfile!: PatientProfile;
  public showConfirmationPopup: boolean = false;
  public errorMessage: string = '';
  public showErrorMessagePopup: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private patientProfileService: PatientProfileService,
  ) {
  }

  ngOnInit() {
    const patientProfileFromState = history.state.patient;
    if (patientProfileFromState) {
      this.typedPatientProfile = {...patientProfileFromState};
      this.originalPatientProfile = {...patientProfileFromState};
    } else {
      console.error('Patient profile data not found in router state.');
      alert('Error loading patient profile details.');
      this.router.navigate(['/admin/patient']);
    }
  }


  public closeConfirmationPopupAndRedirect(): void {
    this.showConfirmationPopup = false;
    this.router.navigate(['/admin/patient']);
  }

  public closeErrorMessagePopup(): void {
    this.showErrorMessagePopup = false;
  }

  private checkForChangesInEmail(): boolean {
    return this.typedPatientProfile.email !== this.originalPatientProfile.email;
  }

  private checkForChangesInPhoneNumber(): boolean {
    return this.typedPatientProfile.phoneNumber !== this.originalPatientProfile.phoneNumber;
  }

  private checkForChangesInFirstName(): boolean {
    return this.typedPatientProfile.firstName !== this.originalPatientProfile.firstName;
  }

  private checkForChangesInLastName(): boolean {
    return this.typedPatientProfile.lastName !== this.originalPatientProfile.lastName;
  }

  private checkForChangesInBirthDate(): boolean {
    return this.typedPatientProfile.birthDate !== this.originalPatientProfile.birthDate;
  }

  public detectContactInfoChanges(): boolean {
    return this.checkForChangesInEmail() || this.checkForChangesInPhoneNumber();
  }


  public isAtLeastOneFieldFilled(): boolean {

    let isAtLeastOneFieldFilled = false;

    if (!this.updatedPatientProfile) {
      this.updatedPatientProfile = {} as PatientProfile;
      this.updatedPatientProfile.email = this.typedPatientProfile.email;
    }

    if (this.checkForChangesInEmail()) {
      isAtLeastOneFieldFilled = true;
      this.updatedPatientProfile.email = this.typedPatientProfile.email.trim();
    }

    if (this.checkForChangesInPhoneNumber()) {
      isAtLeastOneFieldFilled = true;
      this.updatedPatientProfile.phoneNumber = this.typedPatientProfile.phoneNumber;

    }

    if (this.checkForChangesInFirstName()) {
      isAtLeastOneFieldFilled = true;
      this.updatedPatientProfile.firstName = this.typedPatientProfile.firstName.trim();
    }


    if (this.checkForChangesInLastName()) {
      isAtLeastOneFieldFilled = true;
      this.updatedPatientProfile.lastName = this.typedPatientProfile.lastName.trim();

    }

    if (this.checkForChangesInBirthDate()) {
      isAtLeastOneFieldFilled = true;
      this.updatedPatientProfile.birthDate = this.typedPatientProfile.birthDate.trim();
    }

    return isAtLeastOneFieldFilled;
  }


  public saveChanges() {
    this.patientProfileService.editPatientProfile(this.updatedPatientProfile, this.originalPatientProfile.email).subscribe(
      () => {

        if (!this.detectContactInfoChanges()) {
          this.router.navigate(['/admin/patient']).then(() => {
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
          console.log('Error updating patient profile:', error);
        }
      }
    );
  }


}
