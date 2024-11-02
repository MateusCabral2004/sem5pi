import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { AdminMenuComponent } from './Components/admin-home/admin-home.component';
import { MenuBoxComponent } from './Components/menu-box/menu-box.component';
import { ProfilePictureMenuComponent}  from './Components/profile-picture-menu/profile-picture-menu.component';
import { routes } from './app.routes';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminMenuComponent,
    MenuBoxComponent,
    ProfilePictureMenuComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // Add HttpClientModule here
    RouterModule.forRoot(routes)
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
