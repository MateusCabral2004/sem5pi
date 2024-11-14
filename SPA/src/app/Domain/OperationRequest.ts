import {OperationType} from './OperationType';

export interface OperationRequest{

  patientId:string;
  doctorId:string;
  operationType:OperationType[];
  deadline:string;
  priority:string;
}
