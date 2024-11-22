export interface PatientProfile {
  email?: string;
  phoneNumber?: string;
  firstName?: string;
  lastName?: string;
  birthDate?: string;
  gender?: string;
  allergiesAndMedicalConditions?: string[];
  emergencyContact?: string;
  appointmentHistory?: string[];
}
