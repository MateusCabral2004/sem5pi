import { Component, OnInit } from '@angular/core';
import { OperationTypeService } from '../../services/OperationTypeService/operation-type.service';
import { OperationType } from '../../Domain/OperationType';
import { Router } from '@angular/router';

@Component({
  selector: 'app-operation-type-management',
  templateUrl: './operation-type-management.component.html',
  styleUrls: ['./operation-type-management.component.css']
})
export class OperationTypeManagementComponent implements OnInit {
  operationTypes: OperationType[] = [];

  constructor(private operationTypeService: OperationTypeService, private router: Router) {}

  ngOnInit() {
    this.loadOperationTypes();
  }

  loadOperationTypes() {
    this.operationTypeService.listOperationTypes().subscribe(
      (operationTypes) => {
        this.operationTypes = operationTypes;
      },
      (error) => {
        console.error('Failed to load operation types:', error);
      }
    );
  }

  addOperationType() {
    this.router.navigate(['admin/operationTypeManagement/add']);
  }

  deleteOperationType(op: OperationType) {
    const confirmed = confirm(`Are you sure you want to delete ${op.operationName}?`);
    if (confirmed) {
      this.operationTypeService.deleteOperationType(op).subscribe(
        () => {
          this.loadOperationTypes();
        },
        error => {
          console.error('Error deleting Operation Type:', error);
          alert('Error deleting Operation Type: ' + (error.error || 'An unknown error occurred.'));
        }
      );
    }
  }

  editOperationType(op: OperationType) {
    this.router.navigate(['admin/operationTypeManagement/edit'], { state: { operation: op } });
  }

  viewOperationType(op: OperationType) {
    this.router.navigate(['admin/operationTypeManagement/view'], { state: { operation: op } });
  }

}
