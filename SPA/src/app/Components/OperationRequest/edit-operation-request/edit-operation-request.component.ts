import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {OperationRequestService} from '../../../services/OperationRequestService/operation-request.service';
import {OperationRequest} from '../../../Domain/OperationRequest';

@Component({
  selector: 'app-edit-operation-request',
  templateUrl: './edit-operation-request.component.html',
  styleUrl: './edit-operation-request.component.css',
  standalone: false
})

export class EditOperationRequestComponent implements OnInit{

  public updatedOperationRequest!: OperationRequest;
  public originalOperationRequest!: OperationRequest;
  public operationRequestId:string='';
  @Output() editRequest = new EventEmitter<OperationRequest>();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private operationRequestService: OperationRequestService,
  ) {
  }

  ngOnInit() {
    const operationRequestFromState = history.state.operationRequest;
    if (operationRequestFromState) {
      this.updatedOperationRequest = {...operationRequestFromState};
      this.originalOperationRequest = {...operationRequestFromState};
    } else {
      console.error('Operation request data not found in router state.');
      alert('Error loading operation request details.');
      this.router.navigate(['/doctor/operationRequest']);
    }
  }

  saveChanges() {
    console.log('Saving changes:', this.updatedOperationRequest);
    this.updateDeadline();
    this.updatePriority();
    this.router.navigate(['/doctor/operationRequest']);
  }

  private updateDeadline() {
    if(this.updatedOperationRequest.deadline !== this.originalOperationRequest.deadline) {
      this.operationRequestService.editOperationRequestDeadline(this.updatedOperationRequest,this.operationRequestId,this.updatedOperationRequest.deadline).subscribe();
    }
  }

  private updatePriority() {
    if(this.updatedOperationRequest.priority !== this.originalOperationRequest.priority) {
      this.operationRequestService.editOperationRequestPriority(this.updatedOperationRequest,this.operationRequestId,this.updatedOperationRequest.priority).subscribe();
    }
  }

  isOperationValid(): boolean {
    return true;
  }



}
