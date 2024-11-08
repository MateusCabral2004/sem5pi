import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { OperationType } from '../../Domain/OperationType';
import { OperationTypeService } from '../../services/OperationTypeService/operation-type.service';
import { RequiredStaff } from '../../Domain/RequiredStaff';

@Component({
  selector: 'app-edit-op-type',
  templateUrl: './edit-operation-type.component.html',
  styleUrls: ['./edit-operation-type.component.css']
})
export class EditOperationTypeComponent implements OnInit {
  updatedOperation!: OperationType;
  originalOperation!: OperationType;

  newSpecialization: string = '';
  newNumberOfStaff: number = 0;

  addedStaff: RequiredStaff[] = [];
  removedStaff: RequiredStaff[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private operationTypeService: OperationTypeService
  ) {}

  ngOnInit() {
    const operationFromState = history.state.operation;
    if (operationFromState) {
      this.updatedOperation = { ...operationFromState };
      this.originalOperation = { ...operationFromState };
    } else {
      console.error('Operation data not found in router state.');
      alert('Error loading operation details.');
      this.router.navigate(['/admin/operationTypeManagement']);
    }
  }

  addNewStaff() {
    console.log('Add Staff action triggered');
    console.log(`Specialization: ${this.newSpecialization}, Number of Staff: ${this.newNumberOfStaff}`);

    const newStaff: RequiredStaff = {
      specialization: this.newSpecialization,
      numberOfStaff: this.newNumberOfStaff
    };

    this.updatedOperation.requiredStaff.push(newStaff);

    this.addedStaff.push(newStaff);

    this.newSpecialization = '';
    this.newNumberOfStaff = 0;
  }

  removeStaff(index: number) {
    const staffToRemove = this.updatedOperation.requiredStaff[index];
    console.log('Remove Staff action triggered');
    console.log(`Specialization: ${staffToRemove.specialization}`);

    this.removedStaff.push(staffToRemove);

    this.updatedOperation.requiredStaff.splice(index, 1);
  }

  saveChanges() {
    this.checkUpdateRequiredStaff();
    console.log('Changes saved', this.updatedOperation);
    console.log('Added Staff:', this.addedStaff);
    console.log('Removed Staff:', this.removedStaff);
  }

  private checkUpdateRequiredStaff() {
    const updatedStaff = this.updatedOperation.requiredStaff;
    const originalStaff = this.originalOperation.requiredStaff;

    for (let i = 0; i < originalStaff.length; i++) {
      const original = originalStaff[i];
      const updatedIndex = updatedStaff.findIndex(updated => updated.specialization === original.specialization);
      if (updatedIndex === -1) {
        console.log(`Removing staff: ${original.specialization}`);
        updatedStaff.splice(i, 1);
        this.removedStaff.push(original);
        i--;
      }
    }

    console.log('Updated staff:', updatedStaff);
    console.log('Original staff after update:', originalStaff);
  }

  isOperationValid(): boolean {
    return true;
  }
}
