import {Component, Inject} from '@angular/core';
import {AuthService} from '../../services/AuthService/auth.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrls: ['./patient-management.component.css']
})

export class PatientManagementComponent{
  private auth: AuthService;


  menuItems=[
    {title:'Create Patient Profile', icon:'assets/icon/dashboard.png', link:'/add'},
    {title:'Update Patient Profile', icon:'assets/icon/dashboard.png', link:'/update'},
    {title:'Delete Patient Profile', icon:'assets/icon/dashboard.png', link:'/delete'}
  ];

  constructor(@Inject(AuthService) auth: AuthService, private router:Router) {
    this.auth=auth;
    this.validateUserRole();
  }

  private validateUserRole(){
    const expectedRole='Admin';
    this.auth.validateUserRole(expectedRole);
  }
}
