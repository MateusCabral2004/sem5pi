import {RequiredStaff} from './RequiredStaff';

export interface OperationType{

  operationName: string;
  requiredStaff: RequiredStaff[];
  setupDuration: string;
  surgeryDuration: string;
  cleaningDuration: string;

}
