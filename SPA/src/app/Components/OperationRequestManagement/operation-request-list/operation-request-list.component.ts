import {Component, EventEmitter, Input, Output} from '@angular/core';
import {OperationRequest} from '../../../Domain/OperationRequest';

@Component({
  selector: 'app-operation-request-list',
  templateUrl: './operation-request-list.component.html',
  styleUrl: './operation-request-list.component.css',
  standalone: false
})
export class OperationRequestListComponent {
  @Input() operationRequestList: OperationRequest[] = [];
//  @Output() editOperationRequest = new EventEmitter<OperationRequest>();

}
