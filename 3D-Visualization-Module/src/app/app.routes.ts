import {Routes} from '@angular/router';
import {CubeComponent} from './cube/cube.component';
import {OperationFloorComponent} from "./operation-floor/operation-floor.component";

export const routes: Routes = [
    {path: '', redirectTo: '/hospitalFloor', pathMatch: 'full'},
    {path: 'hospitalFloor', component: OperationFloorComponent},
    {path: 'cube', component: CubeComponent},
];

