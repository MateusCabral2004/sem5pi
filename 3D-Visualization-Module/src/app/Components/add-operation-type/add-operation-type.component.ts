import { Component } from '@angular/core';
import { OperationTypeService } from '../../services/OperationTypeService/operation-type.service';
import { OperationType } from '../../Domain/OperationType';
import { RequiredStaff } from '../../Domain/RequiredStaff';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-op-type',
  templateUrl: './add-operation-type.component.html',
  styleUrls: ['./add-operation-type.component.css']
})
export class AddOperationTypeComponent {
  operation: OperationType = {
    operationName: '',
    requiredStaff: [],
    setupDuration: '',
    surgeryDuration: '',
    cleaningDuration: ''
  };

  newRequiredStaff: RequiredStaff = {
    specialization: '',
    numberOfStaff: 0
  };

  constructor(private operationTypeService: OperationTypeService, private router: Router) {}

  addRequiredStaff() {
    if (this.newRequiredStaff.specialization && this.newRequiredStaff.numberOfStaff > 0) {
      this.operation.requiredStaff.push({ ...this.newRequiredStaff });
      this.newRequiredStaff = { specialization: '', numberOfStaff: 0 }; // Reset
    } else {
      alert('Please provide a valid specialization and number of staff.');
    }
  }

  removeRequiredStaff(index: number) {
    this.operation.requiredStaff.splice(index, 1);
  }

  addOperationType() {
    this.operationTypeService.addOperationType(this.operation).subscribe(
      (response) => {
        alert('Operation Type added successfully!');
        this.router.navigate(['/admin/operationTypeManagement']);
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
      operationName: '',
      requiredStaff: [],
      setupDuration: '',
      surgeryDuration: '',
      cleaningDuration: ''
    };

    this.newRequiredStaff = {
      specialization: '',
      numberOfStaff: 0
    };
  }

  isOperationTypeValid(): boolean {
    return (
      !!this.operation.operationName &&
      this.operation.setupDuration !== '' &&
      this.operation.surgeryDuration !== '' &&
      this.operation.cleaningDuration !== '' &&
      this.operation.requiredStaff.length > 0
    );
  }

  isRequiredStaffValid(): boolean {
    return !!this.newRequiredStaff.specialization && this.newRequiredStaff.numberOfStaff > 0;
  }
}
