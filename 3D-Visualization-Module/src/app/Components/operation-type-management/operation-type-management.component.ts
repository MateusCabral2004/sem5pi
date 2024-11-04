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
    this.operationTypeService.listOperationTypes().subscribe(
      (operationTypes) => {
        this.operationTypes = operationTypes;
        console.log('Fetched operation types:', this.operationTypes);
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
    alert(`Deleting ${op.operationName}`);
    // Implement delete logic here
  }

  editOperationType(op: OperationType) {
    alert(`Editing ${op.operationName}`);
    // Implement edit logic here
  }
}
