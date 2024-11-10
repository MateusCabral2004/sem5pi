import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {OperationType} from '../../Domain/OperationType';
import {OperationTypeService} from '../../services/OperationTypeService/operation-type.service';
import {RequiredStaff} from '../../Domain/RequiredStaff';

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
    ) {
    }

    ngOnInit() {
        const operationFromState = history.state.operation;
        if (operationFromState) {
            this.updatedOperation = {...operationFromState};
            this.originalOperation = {...operationFromState};
        } else {
            console.error('Operation data not found in router state.');
            alert('Error loading operation details.');
            this.router.navigate(['/admin/operationTypeManagement']);
        }
    }

    addNewStaff() {

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

        this.removedStaff.push(staffToRemove);

        this.updatedOperation.requiredStaff.splice(index, 1);
    }

    saveChanges() {
        console.log('Saving changes:', this.updatedOperation);
        this.updateRequiredStaff();
        this.updateOperationSetupDuration();
        this.updateOperationCleaningDuration();
        this.updateOperationSurgeryDuration();
        this.updateOperationName();
        this.router.navigate(['/admin/operationTypeManagement']);
    }


    private updateRequiredStaff() {
        this.removedStaff.forEach((staff, index) => {
            this.operationTypeService.removeRequiredStaff(this.originalOperation, staff).subscribe(() => {
                this.removedStaff.splice(index, 1);
            });
        });

        this.addedStaff.forEach((staff, index) => {
            this.operationTypeService.addRequiredStaff(this.originalOperation, staff).subscribe(() => {
                this.addedStaff.splice(index, 1);
            });
        });
    }

    private updateOperationSetupDuration() {
        if(this.updatedOperation.setupDuration !== this.originalOperation.setupDuration) {
            this.operationTypeService.updateDuration(this.originalOperation.operationName,this.updatedOperation.setupDuration,"setup").subscribe();
        }
    }

    private updateOperationCleaningDuration() {
        if(this.updatedOperation.cleaningDuration !== this.originalOperation.cleaningDuration) {
            this.operationTypeService.updateDuration(this.originalOperation.operationName,this.updatedOperation.setupDuration,"cleaning").subscribe();
        }
    }

    private updateOperationSurgeryDuration() {
        if(this.updatedOperation.surgeryDuration !== this.originalOperation.surgeryDuration) {
            this.operationTypeService.updateDuration(this.originalOperation.operationName,this.updatedOperation.setupDuration,"surgery").subscribe();
        }
    }

    private updateOperationName() {
        if(this.updatedOperation.operationName !== this.originalOperation.operationName) {
            this.operationTypeService.updateOperationName(this.originalOperation.operationName,this.updatedOperation.operationName).subscribe();
        }
    }

    isOperationValid(): boolean {
        return true;
    }
}
