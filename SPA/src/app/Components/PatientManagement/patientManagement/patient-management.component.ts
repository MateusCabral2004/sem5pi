import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';
import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../../services/AuthService/auth.service';
import {ConfirmModalComponent} from '../../Shared/confirm-modal/confirm-modal.component';
import {Router} from '@angular/router';
import {EnterFilterNameComponent} from '../../Shared/enter-filter-name/enter-filter-name.component';
import {PatientsListing} from '../../../Domain/PatientsListing';
import {OperationType} from '../../../Domain/OperationType';
import {PatientProfile} from '../../../Domain/PatientProfile';


@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrls: ['./patient-management.component.css']
})
export class PatientManagementComponent implements OnInit {
  public patientProfilesList: PatientsListing[] = [];
  private auth: AuthService;
  private patientProfileService: PatientProfileService;
  private patientId: string = '';
  public showPatientProfileList: boolean = true;
  public showNoPatientProfilesFound: boolean = false;
  public showResetFilterButton: boolean = false;
  public currentFilter: string = '';
  public showFilterPatientButton: boolean = true;
  public showEmptyNameError: boolean = false;
  public showEmptyEmailError: boolean = false;
  public showEmptyDateBirthError: boolean = false;
  public showEmptyMedicalRecordError: boolean = false;
  patientProfileToDelete: PatientProfile | null = null;


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
  public resetFilter() {
    this.patientProfilesList = [];
    this.showFilterPatientButton = true;
    this.showPatientProfileList = true;
    this.showEmptyEmailError = false;
    this.showEmptyNameError = false;
    this.showEmptyDateBirthError = false;
    this.fetchPatientProfiles();
    this.showNoPatientProfilesFound = false;
    this.showResetFilterButton = false;
  }


  public handleSelectedFilter(filter: string) {

    if(filter === 'Name') {
      this.enterFilterName.open("name");
    }

    if(filter === 'Email') {
      this.enterFilterName.open("email");
    }
  }
  confirmDeletePatientProfile(patientId: string) {
    this.patientId = patientId;
    this.confirmModal.open("Are you sure you want to proceed?");
  }

  handleDeletePatientProfileConfirmation(isConfirmed: boolean) {
    if (isConfirmed && this.patientProfileToDelete !== null) {
      this.patientProfileService.deletePatientProfile(this.patientProfileToDelete).subscribe(
        () => {
          this.fetchPatientProfiles();
        },
        error => {
          console.error('Error deleting Patient Profile:', error);
          alert('Error deleting Patient Profile: ' + (error.error || 'An unknown error occurred.'));
        }
      );
      this.patientProfileToDelete = null;
    }
  }

  public fetchPatientProfiles() {
    this.patientProfileService.listAllPatientProfiles().subscribe(
      (data: PatientsListing[]) => {
        this.patientProfilesList = data;
      },
      (error) => {
        console.error('Error fetching patient profiles', error);
      }
    );
  }

}
