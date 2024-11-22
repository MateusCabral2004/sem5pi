import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PatientProfile } from '../../Domain/PatientProfile';
import { PatientsListing } from '../../Domain/PatientsListing';
import {PatientProfileService} from './patient-profile-service';

describe('PatientProfileService', () => {
  let service: PatientProfileService;
  let httpMock: HttpTestingController;

  const apiUrl = 'http://localhost:5001';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PatientProfileService],
    });

    service = TestBed.inject(PatientProfileService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('registerPatientProfile', () => {
    it('should call POST to register a patient profile', () => {
      const mockProfile: PatientProfile = {
        firstName: 'John',
        lastName: 'Doe',
        email: 'john.doe@example.com',
        birthDate: '1990-01-01',
        phoneNumber: 1234567890,
        gender: 'Male',
        emergencyContact: 'Jane Doe - 9876543210',
      };
      const mockResponse = 'Patient registered successfully';

      service.registerPatientProfile(mockProfile).subscribe((response) => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/registerPatientProfile`);
      expect(req.request.method).toBe('POST');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockResponse);
    });

    it('should handle errors on patient profile registration', () => {
      const mockProfile: PatientProfile = {
        firstName: 'John',
        lastName: 'Doe',
        email: 'john.doe@example.com',
        birthDate: '1990-01-01',
        phoneNumber: 1234567890,
        gender: 'Male',
        emergencyContact: 'Jane Doe - 9876543210',
      };

      service.registerPatientProfile(mockProfile).subscribe((response) => {
        expect(response).toBe('');
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/registerPatientProfile`);
      req.error(new ErrorEvent('Network error'));
    });
  });

  describe('deletePatientProfile', () => {
    it('should call DELETE to remove a patient profile', () => {
      const email = 'test@example.com';
      const mockResponse = 'Patient profile deleted';

      service.deletePatientProfile(email).subscribe((response) => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/deletePatientProfile/${email}`);
      expect(req.request.method).toBe('DELETE');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockResponse);
    });

    it('should handle errors on deleting a patient profile', () => {
      const email = 'test@example.com';

      service.deletePatientProfile(email).subscribe((response) => {
        expect(response).toBe('');
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/deletePatientProfile/${email}`);
      req.error(new ErrorEvent('Network error'));
    });
  });

  describe('listAllPatientProfiles', () => {
    it('should call GET to retrieve all patient profiles', () => {
      const mockProfiles: PatientsListing[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', birthDate: '1990-01-01' },
        { id: '2', fullName: 'Jane Smith', email: 'jane.smith@example.com', birthDate: '1985-05-15' },
      ];

      service.listAllPatientProfiles().subscribe((profiles) => {
        expect(profiles).toEqual(mockProfiles);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient`);
      expect(req.request.method).toBe('GET');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockProfiles);
    });
  });

  describe('filterPatientProfilesByName', () => {
    it('should call GET to filter patient profiles by name', () => {
      const name = 'John';
      const mockProfiles: PatientsListing[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', birthDate: '1990-01-01' },
      ];

      service.filterPatientProfilesByName(name).subscribe((profiles) => {
        expect(profiles).toEqual(mockProfiles);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/listPatientProfilesByName/${name}`);
      expect(req.request.method).toBe('GET');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockProfiles);
    });
  });

  describe('filterPatientProfilesByEmail', () => {
    it('should call GET to filter patient profiles by email', () => {
      const email = 'john.doe@example.com';
      const mockProfiles: PatientsListing[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', birthDate: '1990-01-01' },
      ];

      service.filterPatientProfilesByEmail(email).subscribe((profiles) => {
        expect(profiles).toEqual(mockProfiles);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/listPatientProfilesByEmail/${email}`);
      expect(req.request.method).toBe('GET');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockProfiles);
    });
  });

  describe('filterPatientProfilesByBirthDate', () => {
    it('should call GET to filter patient profiles by birth date', () => {
      const birthDate = '1990-01-01';
      const mockProfiles: PatientsListing[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', birthDate: '1990-01-01' },
      ];

      service.filterPatientProfilesByBirthDate(birthDate).subscribe((profiles) => {
        expect(profiles).toEqual(mockProfiles);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/listPatientProfilesByDateOfBirth/${birthDate}`);
      expect(req.request.method).toBe('GET');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockProfiles);
    });
  });

  describe('filterPatientProfilesByMedicalRecordNumber', () => {
    it('should call GET to filter patient profiles by medical record number', () => {
      const id = 'MRN123';
      const mockProfile: PatientsListing = {
        id: '1',
        fullName: 'John Doe',
        email: 'john.doe@example.com',
        birthDate: '1990-01-01',
      };

      service.filterPatientProfilesByMedicalRecordNumber(id).subscribe((profile) => {
        expect(profile).toEqual(mockProfile);
      });

      const req = httpMock.expectOne(`${apiUrl}/Patient/listPatientProfilesByMedicalRecordNumber/${id}`);
      expect(req.request.method).toBe('GET');
      expect(req.request.withCredentials).toBeTrue();
      req.flush(mockProfile);
    });
  });

  describe('editPatientProfile', () => {
    it('should call PATCH to update a patient profile', () => {
      const editedProfile: PatientProfile = {
        firstName: 'John',
        lastName: 'Doe',
        email: 'john.doe@example.com',
        birthDate: '1990-01-01',
        phoneNumber: 1234567890,
        gender: 'Male',
        emergencyContact: 'Jane Doe - 9876543210',
      };

      service.editPatientProfile(editedProfile).subscribe();

      const req = httpMock.expectOne(`${apiUrl}/Patient`);
      expect(req.request.method).toBe('PATCH');
      expect(req.request.withCredentials).toBeTrue();
      req.flush({});
    });
  });
});
