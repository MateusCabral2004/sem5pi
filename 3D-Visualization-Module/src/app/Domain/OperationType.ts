import {RequiredStaff} from './RequiredStaff';

export interface OperationType{

  OperationName: string;
  RequiredStaff: RequiredStaff[];
  SetupDuration: string;
  SurgeryDuration: string;
  CleaningDuration: string;

}
