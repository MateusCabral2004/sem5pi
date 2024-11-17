import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {CubeComponent} from "./cube/cube.component";
import {routes} from './app.routes';
import {AppComponent} from "./app.component";
import {SurgeryRoomComponent} from "./surgery-room/surgery-room.component";
import {OperationFloorComponent} from "./operation-floor/operation-floor.component";
import {ValidateMap} from './validateMap/validateMap';
import {CorridorComponent} from './corridor/corridor.component';
import {EdgeWallComponent} from './edge-wall/edge-wall.component';

@NgModule({
  declarations: [
    AppComponent,
    CubeComponent,
    SurgeryRoomComponent,
    OperationFloorComponent,
    CorridorComponent,
    EdgeWallComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
  ],
  providers: [ValidateMap],
  bootstrap: [AppComponent]
})
export class AppModule {
}
