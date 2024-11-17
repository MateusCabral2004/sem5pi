import {ActivatedRoute, Router} from '@angular/router';
import {Component, OnInit} from '@angular/core';
import {PatientProfile} from '../../../Domain/PatientProfile';
import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';


@Component({
  selector: 'app-register-patient-profile',
  templateUrl: './register-patient-profile.component.html',
  styleUrls: ['./register-patient-profile.component.css']
})

export class RegisterPatientProfileComponent implements OnInit {

  public newPatientProfile!: PatientProfile;
  public showErrorMessagePopup: boolean = false;
  public errorMessage: string = '';


  constructor(private router: Router,
              private route: ActivatedRoute,
              private patientProfileService: PatientProfileService,
  ) {
  }

  ngOnInit() {
    this.newPatientProfile.firstName='';
    this.newPatientProfile.lastName='';
    this.newPatientProfile.phoneNumber=0;
    this.newPatientProfile.medicalHistory='';
    this.newPatientProfile.emailAddress='';
    this.newPatientProfile.dateOfBirth='';
  }

  private checkIfFirstNameIsFilled():boolean{
    return this.newPatientProfile.firstName.trim()!=='';
  }

  private checkIfLastNameIsFilled():boolean{
    return this.newPatientProfile.lastName.trim()!=='';
  }

  private checkIfPhoneNumberIsFilled():boolean{
    return this.newPatientProfile.phoneNumber!=0;
  }

  private checkIfMedicalHistoryIsFilled():boolean{
    return this.newPatientProfile.lastName.trim()!=='';
  }

  private checkIfEmailAddressIsFilled():boolean{
    return this.newPatientProfile.lastName.trim()!=='';
  }

  private checkIfDateOfBirthIsFilled():boolean{
    return this.newPatientProfile.lastName.trim()!=='';
  }

  public areAllFieldsFilled(): boolean {
    return this.checkIfFirstNameIsFilled() && this.checkIfLastNameIsFilled() && this.checkIfPhoneNumberIsFilled() && this.checkIfMedicalHistoryIsFilled() && this.checkIfEmailAddressIsFilled() && this.checkIfDateOfBirthIsFilled();
  }


  public registerPatientProfile(){
      this.patientProfileService.registerPatientProfile(this.newPatientProfile).subscribe(
        () => {
          this.router.navigate(['/admin/patient']).then(() => {
            window.location.reload();
          });

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

          } else if (error.status === 604) {

            console.log('Error registering the patient profile:', error);

            this.errorMessage = error.error;
            this.showErrorMessagePopup = true;

          } else if (error.status === 605) {

            this.errorMessage = error.error;
            this.showErrorMessagePopup = true;

          } else {
            console.log('Error updating patient profile:', error);
          }
        }
      );

  }

  public closeErrorMessagePopup()
    :
    void {
    this.showErrorMessagePopup = false;
  }
}
