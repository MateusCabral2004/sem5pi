import {PatientProfileService} from '../../../services/PatientProfileService/patient-profile-service';
import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../../services/AuthService/auth.service';
import {ConfirmModalComponent} from '../../Shared/confirm-modal/confirm-modal.component';
import {Router} from '@angular/router';
import {EnterFilterNameComponent} from '../../Shared/enter-filter-name/enter-filter-name.component';
import {PatientsListing} from '../../../Domain/PatientsListing';


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

  // public applyNameFilter(filterName: string) {
  //
  //   const trimmedFilterName = filterName.trim();
  //
  //   this.patientProfileService.filterPatientProfilesByName(trimmedFilterName).subscribe(
  //
  //     (data: PatientProfile[]) => {
  //       this.showNoPatientProfilesFound = false;
  //       this.showPatientProfileList = true;
  //       this.patientProfilesList = data;
  //       this.showResetFilterButton = true;
  //     },
  //     (error) => {
  //
  //       if (error.status === 404) {
  //
  //         this.patientProfilesList = [];
  //         this.showPatientProfileList = false;
  //         this.showNoPatientProfilesFound = true;
  //         this.showResetFilterButton = true;
  //
  //       } else {
  //
  //         this.showPatientProfileList = false;
  //         this.showEmptyNameError = true;
  //         this.showResetFilterButton = true;
  //       }
  //     }
  //   );
  // }
  //
  // public applyEmailFilter(filterEmail: string) {
  //
  //   const trimmedFilterEmail = filterEmail.trim();
  //
  //   this.patientProfileService.filterPatientProfilesByEmail(trimmedFilterEmail).subscribe(
  //
  //     (data: PatientProfile) => {
  //       this.showNoPatientProfilesFound = false;
  //       this.showPatientProfileList = true;
  //       this.patientProfilesList = [];
  //       this.patientProfilesList.push(data);
  //       this.showResetFilterButton = true;
  //     },
  //     (error) => {
  //
  //       if (error.status === 404) {
  //
  //         this.patientProfilesList = [];
  //         this.showPatientProfileList = false;
  //         this.showNoPatientProfilesFound = true;
  //         this.showResetFilterButton = true;
  //       }
  //       else {
  //
  //         this.showPatientProfileList = false;
  //         this.showEmptyEmailError = true;
  //         this.showResetFilterButton = true;
  //
  //       }
  //     }
  //   );
  // }


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

  // public handleFilterSelection(filterValue: string) {
  //
  //   this.showFilterPatientButton = false;
  //   this.showEmptyNameError = false;
  //   this.showEmptyEmailError = false;
  //
  //
  //   if (this.currentFilter === 'name') {
  //     this.applyNameFilter(filterValue);
  //   } else if (this.currentFilter === 'email') {
  //     this.applyEmailFilter(filterValue);
  //   }
  // }

  public handleSelectedFilter(filter: string) {

    if(filter === 'Name') {
      this.enterFilterName.open("name");
    }

    if(filter === 'Email') {
      this.enterFilterName.open("email");
    }
  }
  public confirmDeletePatientProfile(patientId: string) {
    this.patientId = patientId;
    this.confirmModal.open("Are you sure you want to proceed?");
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
