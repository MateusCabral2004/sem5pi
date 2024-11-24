import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Router} from '@angular/router';
import {PatientService} from '../../../services/PatientService/patient.service';


@Component({
  selector: 'app-register-patient',
  templateUrl: './register-patient.component.html',
  styleUrls: ['./register-patient.component.css'],
  standalone: false

})
export class RegisterPatientComponent {
  registrationForm: FormGroup;

  constructor(private router: Router,private fb: FormBuilder,private userService: PatientService) {
    this.registrationForm = this.fb.group({
      phoneNumber: ['', [Validators.required, this.phoneNumberValidator]], // Add custom validator
    });
  }

   phoneNumberValidator(control: any) {
    const phonePattern = /^[0-9]*$/;
    if (control.value && !phonePattern.test(control.value)) {
      return { invalidPhoneNumber: true };
    }
    return null; // Return null if valid
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      const number = this.registrationForm.value.phoneNumber;
      this.userService.registerNumber(number).subscribe(
        response => {
          alert('Registration was successful! Please verify your account via email.');
          console.log('response', response);
          window.location.href = 'http://localhost:5001/login/logout';
        },
        error => {
          console.error('Erro ao registrar patient:', error);
          const errorMessage = error?.error?.message || 'Erro desconhecido';
          alert('Erro ao registrar patient: ' + errorMessage+number);
        }
      );
    } else {
      alert('Por favor, insira um número de telefone válido.');
    }
  }


}
