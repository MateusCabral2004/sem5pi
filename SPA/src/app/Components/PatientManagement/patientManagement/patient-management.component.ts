import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';
import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../../services/AuthService/auth.service';
import {ConfirmModalComponent} from '../../Shared/confirm-modal/confirm-modal.component';
import {Router} from '@angular/router';
import {EnterFilterNameComponent} from '../../Shared/enter-filter-name/enter-filter-name.component';
import {PatientProfile} from '../../../Domain/PatientProfile';



@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrls: ['./patient-management.component.css']
})
export class PatientManagementComponent implements OnInit {
  public patientProfilesList: PatientProfile[] = [];
  private auth: AuthService;
  private patientProfileService: PatientProfileService;
  private patientId: string = '';
  public showPatientProfileList: boolean = true;
  public showNoPatientProfilesFound: boolean = false;
  public showResetFilterButton: boolean = false;
  public showFilterPatientButton: boolean = true;
  public showEmptyNameError: boolean = false;
  public showEmptyEmailError: boolean = false;
  public showEmptyDateBirthError: boolean = false;
  public showEmptyMedicalRecordNumberError: boolean = false;
  currentFilter: string = '';


  @ViewChild(ConfirmModalComponent) confirmModal!: ConfirmModalComponent;
  @ViewChild(EnterFilterNameComponent) enterFilterName!: EnterFilterNameComponent;


  constructor(@Inject(AuthService) auth: AuthService, @Inject(PatientProfileService) patientProfileService: PatientProfileService, private router: Router) {
    this.auth = auth;
    this.patientProfileService = patientProfileService;
    this.validateUserRole();
  }

  ngOnInit(): void {
    this.fetchPatientProfiles();
  }

  private validateUserRole() {
    const expectedRole = "Admin";
    this.auth.validateUserRole(expectedRole);
  }

  public registerPatientProfile() {
    this.router.navigate(['admin/patient/register']);
  }

  confirmDeletePatientProfile(patientId: string) {
    this.patientId = patientId;
    this.confirmModal.open("Are you sure you want to proceed?");
  }

  public handleDeletePatientProfileConfirmation(isConfirmed: boolean) {

    if (isConfirmed) {
      this.patientProfileService.deletePatientProfile(this.patientId).subscribe(
        response => {
          console.log('Patient profile deactivated:', response);

          this.patientProfilesList = this.patientProfilesList.filter(patient => patient.email !== this.patientId);
        },
        error => {

          if (error.status === 404) {
            console.log(error);
          } else {
            console.error('Error deactivating patient profile:', error);
          }
        }
      );
    }

  }

  public fetchPatientProfiles() {
    this.patientProfileService.listAllPatientProfiles().subscribe(
      (data: PatientProfile[]) => {
        this.patientProfilesList = data;
        console.log(this.patientProfilesList);
      },
      (error) => {
        console.error('Error fetching patient profiles', error);
      }
    );
  }

  public handleSelectedFilter(filter: string) {

    if (filter === 'Name') {
      this.enterFilterName.open("name");
    }
    if (filter === 'Email') {
      this.enterFilterName.open("email");
    }
    if (filter === 'Medical Record Number') {
      this.enterFilterName.open("id");
    }
    if (filter === 'Birth Date') {
      this.enterFilterName.open("birthDate");
    }
  }

  public handleFilterSelection(filterValue: string) {

    this.showFilterPatientButton = false;
    this.showEmptyNameError = false;
    this.showEmptyEmailError = false;
    this.showEmptyMedicalRecordNumberError = false;
    this.showEmptyDateBirthError = false;

    if (this.currentFilter === 'name') {
      this.applyNameFilter(filterValue);
    }
    if (this.currentFilter === 'email') {
      this.applyEmailFilter(filterValue);
    }
    if (this.currentFilter === 'id') {
      this.applyMedicalRecordNumberFilter(filterValue);
    }
    if (this.currentFilter === 'birthDate') {
      this.applyBirthDateFilter(filterValue);
    }
  }

  public resetFilter() {
    this.patientProfilesList = [];
    this.showFilterPatientButton = true;
    this.showPatientProfileList = true;
    this.showEmptyEmailError = false;
    this.showEmptyNameError = false;
    this.showEmptyDateBirthError = false;
    this.showEmptyMedicalRecordNumberError = false;
    this.fetchPatientProfiles();
    this.showNoPatientProfilesFound = false;
    this.showResetFilterButton = false;
  }

  public applyNameFilter(filterName: string) {

    const trimmedFilterName = filterName.trim();

    this.patientProfileService.filterPatientProfilesByName(trimmedFilterName).subscribe(
      (data: PatientProfile[]) => {


        this.showNoPatientProfilesFound = false;
        this.showPatientProfileList = true;
        this.patientProfilesList = data;
        this.showResetFilterButton = true;
      },
      (error) => {
        if (error.status === 404) {
          this.patientProfilesList = [];
          this.showPatientProfileList = false;
          this.showResetFilterButton = true;
          this.showNoPatientProfilesFound = true;

        } else {
          this.showPatientProfileList = false;
          this.showResetFilterButton = true;
          this.showEmptyNameError = true;

        }
      }
    );
  }

  public applyEmailFilter(filterEmail: string) {

    const trimmedFilterEmail = filterEmail.trim();

    this.patientProfileService.filterPatientProfilesByEmail(trimmedFilterEmail).subscribe(
      (data: PatientProfile) => {
        this.showNoPatientProfilesFound = false;
        this.showPatientProfileList = true;
        this.patientProfilesList = [];
        this.patientProfilesList.push(data);
        this.showResetFilterButton = true;
      },
      (error) => {
        if (error.status === 404) {
          this.patientProfilesList = [];
          this.showPatientProfileList = false;
          this.showNoPatientProfilesFound = true;
          this.showResetFilterButton = true;
        } else {
          this.showPatientProfileList = false;
          this.showEmptyEmailError = true;
          this.showResetFilterButton = true;
        }
      }
    );
  }

  public applyMedicalRecordNumberFilter(filterMRN: string) {

    const trimmedMRN = filterMRN.trim();

    this.patientProfileService.filterPatientProfilesByMedicalRecordNumber(trimmedMRN).subscribe(
      (data: PatientProfile) => {
       this.showNoPatientProfilesFound = false;
        this.patientProfilesList = [];
        this.patientProfilesList.push(data);
       this.showPatientProfileList = true;
       this.showResetFilterButton = true;
      },
      (error) => {
        if (error.status === 404) {
          this.patientProfilesList = [];
          this.showPatientProfileList = false;
          this.showNoPatientProfilesFound = true;
          this.showResetFilterButton = true;

        } else {
          this.showPatientProfileList = false;
          this.showEmptyMedicalRecordNumberError = true;
          this.showResetFilterButton = true;
        }
      }
    );
  }

  public applyBirthDateFilter(filterBirthDate: string) {

    const trimmedBirthDate = filterBirthDate.trim();

    this.patientProfileService.filterPatientProfilesByBirthDate(trimmedBirthDate).subscribe(
      (data: PatientProfile[]) => {
        this.showNoPatientProfilesFound = false;
        this.showPatientProfileList = true;
        this.patientProfilesList = data;
        this.showResetFilterButton = true;
      },
      (error) => {
        if (error.status === 404) {
          this.patientProfilesList = [];
          this.showPatientProfileList = false;
          this.showNoPatientProfilesFound = true;
          this.showResetFilterButton = true;

        } else {
          this.showPatientProfileList = false;
          this.showEmptyDateBirthError = true;
          this.showResetFilterButton = true;
        }
      }
    );
  }

  public editPatientProfile(patient: PatientProfile) {
    this.router.navigate(['admin/patient/edit'], { state: { patient: patient } });
  }

}


