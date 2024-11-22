import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {OperationRequest} from '../../../Domain/OperationRequest';
import {ConfirmModalComponent} from '../../Shared/confirm-modal/confirm-modal.component';
import {EnterFilterNameComponent} from '../../Shared/enter-filter-name/enter-filter-name.component';
import {Router} from '@angular/router';
import {OperationRequestService} from '../../../services/OperationRequestService/operation-request.service';
import {AuthService} from '../../../services/AuthService/auth.service';

@Component({
  selector: 'app-operation-request-management',
  templateUrl: './operation-request-management.component.html',
  styleUrls: ['./operation-request-management.component.css'],
  standalone: false
})

export class OperationRequestManagementComponent implements OnInit {
  public operationRequestsList: OperationRequest[] = [];
  private auth: AuthService;
  private operationRequestService: OperationRequestService;
  private operationRequestId: string='';

  @ViewChild(ConfirmModalComponent) confirmModal!: ConfirmModalComponent;
  @ViewChild(EnterFilterNameComponent) enterFilterName!: EnterFilterNameComponent;

  constructor(@Inject(AuthService) auth: AuthService, @Inject(OperationRequestService) operationRequestService: OperationRequestService, private router: Router) {
    this.auth = auth;
    this.operationRequestService = operationRequestService;
    this.validateUserRole();
  }

  ngOnInit() {
    this.fetchOperationRequests();
  }

  public addOperationType() {
    this.router.navigate(['doctor/operationRequestManagement/add']);
  }


  private validateUserRole() {
    const expectedRole = "Doctor";
    this.auth.validateUserRole(expectedRole);
  }

  public fetchOperationRequests() {
    this.operationRequestService.listOperationRequests().subscribe(
      (data: OperationRequest[]) => {
        this.operationRequestsList = data;
        console.log(this.operationRequestsList);
      },
      (error) => {
        console.error('Error fetching operation requests', error);
      }
    );
  }



}
