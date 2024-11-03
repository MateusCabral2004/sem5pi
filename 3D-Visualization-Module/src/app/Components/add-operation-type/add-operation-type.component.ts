import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OperationType } from '../../Domain/OperationType';
import { RequiredStaff } from '../../Domain/RequiredStaff';

@Component({
  selector: 'app-add-op-type',
  templateUrl: './add-operation-type.component.html',
  styleUrls: ['./add-operation-type.component.css']
})
export class AddOperationTypeComponent {
  operation: OperationType = {
    OperationName: '',
    RequiredStaff: [],
    SetupDuration: '',
    SurgeryDuration: '',
    CleaningDuration: ''
  };

  newRequiredStaff: RequiredStaff = {
    Specialization: '',
    NumberOfStaff: 0
  };

  backendUrl = 'http://localhost:5001';

  constructor(private http: HttpClient) {}

  addRequiredStaff() {
    if (this.newRequiredStaff.Specialization && this.newRequiredStaff.NumberOfStaff > 0) {
      this.operation.RequiredStaff.push({ ...this.newRequiredStaff });
      this.newRequiredStaff = { Specialization: '', NumberOfStaff: 0 }; // Reset
    } else {
      alert('Please provide a valid specialization and number of staff.');
    }
  }

  removeRequiredStaff(index: number) {
    this.operation.RequiredStaff.splice(index, 1);
  }

  addOperationType() {
    console.log(this.operation);
    this.http.post(this.backendUrl + '/OperationType/addNewOperationType', this.operation, {
      withCredentials: true,
      responseType: 'text'
    }).subscribe(
      (response) => {
        alert('Operation Type added successfully! It works.');
        this.resetFields();
        console.log('Success:', response);
      },
      (error) => {
        console.error('Error:', error);
        alert('Error adding Operation Type: ' + (error.error || 'An unknown error occurred.'));
      }
    );
  }

  resetFields() {
    this.operation = {
      OperationName: '',
      RequiredStaff: [],
      SetupDuration: '',
      SurgeryDuration: '',
      CleaningDuration: ''
    };

    this.newRequiredStaff = {
      Specialization: '',
      NumberOfStaff: 0
    };
  }

  isOperationTypeValid(): boolean {
    return (
      !!this.operation.OperationName &&
      this.operation.SetupDuration !== '' &&
      this.operation.SurgeryDuration !== '' &&
      this.operation.CleaningDuration !== '' &&
      this.operation.RequiredStaff.length > 0
    );
  }

  isRequiredStaffValid(): boolean {
    return !!this.newRequiredStaff.Specialization && this.newRequiredStaff.NumberOfStaff > 0;
  }
}
