import {Component, Input} from '@angular/core';
import {OperationRequest} from '../../../Domain/OperationRequest';

@Component({
  selector: 'app-operation-request-details',
  templateUrl: './operation-request-details.component.html',
  styleUrl: './operation-request-details.component.css'
})
export class OperationRequestDetailsComponent {
  @Input() operationRequest!: OperationRequest;
}
