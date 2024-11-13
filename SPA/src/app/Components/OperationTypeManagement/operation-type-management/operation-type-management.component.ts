import {Component, OnInit, ViewChild} from '@angular/core';
import { OperationTypeService } from '../../../services/OperationTypeService/operation-type.service';
import { OperationType } from '../../../Domain/OperationType';
import { Router } from '@angular/router';
import {ConfirmModalComponent} from '../../Shared/confirm-modal/confirm-modal.component';
import {EnterFilterNameComponent} from '../../Shared/enter-filter-name/enter-filter-name.component';

@Component({
  selector: 'app-operation-type-management',
  templateUrl: './operation-type-management.component.html',
  styleUrls: ['./operation-type-management.component.css']
})
export class OperationTypeManagementComponent implements OnInit {
  operationTypes: OperationType[] = [];
  currentFilter: string = '';
  filterValue: string = '';
  public showResetFilterButton: boolean = false;
  operationTypeToBeDeleted: OperationType | null = null;

  @ViewChild(ConfirmModalComponent) confirmModal!: ConfirmModalComponent;
  @ViewChild(EnterFilterNameComponent) enterFilterName!: EnterFilterNameComponent;

  constructor(private operationTypeService: OperationTypeService, private router: Router) {}

  ngOnInit() {
    this.loadOperationTypes();
  }

  loadOperationTypes() {

    if(this.currentFilter !== "" && this.filterValue !== "") {
      this.operationTypeService.filterOperationTypes(this.currentFilter,this.filterValue).subscribe(
        (operationTypes) => {
          this.operationTypes = operationTypes;
        },
        (error) => {
          console.error('Failed to load operation types:', error);
        }
      );
      this.showResetFilterButton = true;
      return;
    }

    this.operationTypeService.listOperationTypes().subscribe(
      (operationTypes) => {
        this.operationTypes = operationTypes;
      },
      (error) => {
        console.error('Failed to load operation types:', error);
      }
    );
    this.showResetFilterButton = false;
  }

  addOperationType() {
    this.router.navigate(['admin/operationTypeManagement/add']);
  }

  deleteOperationType(op: OperationType) {
    this.operationTypeToBeDeleted = op;
    this.confirmModal.open('Are you sure you want to delete this Operation Type?');
  }

  editOperationType(op: OperationType) {
    this.router.navigate(['admin/operationTypeManagement/edit'], { state: { operation: op } });
  }

  viewOperationType(op: OperationType) {
    this.router.navigate(['admin/operationTypeManagement/view'], { state: { operation: op } });
  }

  handleSelectedFilter(filter: string) {
    this.currentFilter = filter;
    this.enterFilterName.open(filter);
  }

  handleFilterButtonEvent(filterValue: string) {
    this.filterValue = filterValue;
    this.loadOperationTypes();
  }

  handleDeleteOperationTypeConfirmation(isConfirmed: boolean) {
    if (isConfirmed && this.operationTypeToBeDeleted !== null) {
        this.operationTypeService.deleteOperationType(this.operationTypeToBeDeleted).subscribe(
          () => {
            this.loadOperationTypes();
          },
          error => {
            console.error('Error deleting Operation Type:', error);
            alert('Error deleting Operation Type: ' + (error.error || 'An unknown error occurred.'));
          }
        );
      this.operationTypeToBeDeleted = null;
    }
  }

  public resetFilter() {
    this.currentFilter = '';
    this.filterValue = '';
    this.loadOperationTypes();
  }
}
