import {Routes} from '@angular/router';
import {OperationFloorComponent} from "./operation-floor/operation-floor.component";

export const routes: Routes = [
    {path: '', redirectTo: '/hospitalFloor', pathMatch: 'full'},
    {path: 'hospitalFloor', component: OperationFloorComponent}
];

