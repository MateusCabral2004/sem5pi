import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {CubeComponent} from "./cube/cube.component";
import {routes} from './app.routes';
import {AppComponent} from "./app.component";
import {SurgeryRoomComponent} from "./surgery-room/surgery-room.component";
import {OperationFloorComponent} from "./operation-floor/operation-floor.component";

@NgModule({
    declarations: [
        AppComponent,
        CubeComponent,
        SurgeryRoomComponent,
        OperationFloorComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
