import {Component} from '@angular/core';
import {OperationRequest} from '../../../Domain/OperationRequest';
import {Router} from '@angular/router';
import {OperationRequestService} from '../../../services/OperationRequestService/operation-request.service';

@Component({
  selector: 'app-add-operation-request',
  templateUrl: './add-operation-request.component.html',
  styleUrls: ['./add-operation-request.component.css'],
  standalone: false
})
export class AddOperationRequestComponent {
  operationRequest: OperationRequest = {
    patientId: '',
    doctorId: '',
    operationType:'',
    deadline: '',
    priority: ''
  };



  constructor(private operationRequestService: OperationRequestService, private router: Router) {
  }

  resetFields() {
    this.operationRequest={
      patientId: '',
      doctorId: '',
      operationType:'',
      deadline: '',
      priority: ''
    };

  }



  addOperationRequest() {
    this.operationRequestService.addOperationRequest(this.operationRequest).subscribe(
      (response) => {
        alert('Operation Request added successfully!');
        this.router.navigate(['/doctor/operationRequestManagement']);
        console.log('Success:', response);
      },
      (error) => {
        console.error('Error:', error);
        alert('Error adding Operation Request: ' + (error.error || 'An unknown error occurred.'));
      }
    );
  }

//listar pacientes
//listar operation types da specialization do doctor
  //listagem da prioridade
  //operation name???


}

