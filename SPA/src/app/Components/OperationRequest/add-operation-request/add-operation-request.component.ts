import {Component, OnInit} from '@angular/core';
import {OperationRequest} from '../../../Domain/OperationRequest';
import {OperationRequestService} from '../../../services/OperationRequestService/operation-request.service';
import {Router} from '@angular/router';
import {PatientProfile} from '../../../Domain/PatientProfile';

@Component({
  selector: 'app-add-operation-request',
  templateUrl: './add-operation-request.component.html',
  styleUrls: ['./add-operation-request.component.css'],
  standalone: false
})


export class AddOperationRequestComponent implements OnInit{
    patients: PatientProfile[]=[];
    operationTypes: OperationRequest[]=[];
    operation: OperationRequest={
      patient:'',
      doctorId:'',
      operationType:'',
      deadline:'',
      priority:''};

    constructor(private operationRequestService: OperationRequestService, private router: Router) {}

  addOperationRequest() {
    this.operationRequestService.addOperationRequest(this.operation).subscribe(
      (response) => {
        alert('Operation Type added successfully!');
        this.router.navigate(['/doctor/operationRequest']);
        console.log('Success:', response);
      },
      (error) => {
        console.error('Error:', error);
        alert('Error adding Operation Request: ' + (error.error || 'An unknown error occurred.'));
      }
    );
  }

  isOperationRequestValid(): boolean {
    return (
      !!this.operation.patient &&
      this.operation.operationType !== '' &&
      this.operation.deadline !== '' &&
      this.operation.priority !== '');
  }

  ngOnInit(): void {
   // this.loadPatients();
   // this.loadOperationTypes();
  }

  loadPatients(): void {
    this.operationRequestService.listAllPatientProfilesNames().subscribe(
      (data:PatientProfile[]) =>{
        console.log(data);
          this.patients=data;
      },
      (error) => {
        console.error('Error fetching patients names', error);
      }
    );
  }

  loadOperationTypes(): void {
    this.operationRequestService.listDoctorsOperationRequests().subscribe(
      (data:OperationRequest[]) =>{
        this.operationTypes=data;
      },
      (error) => {
        console.error('Error fetching operation types', error);
      }
    );
  }




}
