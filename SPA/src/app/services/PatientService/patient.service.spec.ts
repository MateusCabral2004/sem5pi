import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PatientService } from './patient.service';

describe('PatientService', () => {
  let service: PatientService;
  let httpMock: HttpTestingController;
  const apiUrl = 'http://localhost:5001/patient';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PatientService]
    });

    service = TestBed.inject(PatientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('Should delete an account', () => {
    service.deleteAccount().subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`${apiUrl}/account/exclude`);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.withCredentials).toBeTrue();

    req.flush({ success: true });
  });

  it('Should register a patient number', () => {
    const testNumber = 12345;

    service.registerNumber(testNumber).subscribe(response => {
      expect(response).toBeInstanceOf(ArrayBuffer);
    });

    const req = httpMock.expectOne(`${apiUrl}/register?number=${testNumber}`);
    expect(req.request.method).toBe('POST');
    expect(req.request.responseType).toBe('arraybuffer');

    req.flush(new ArrayBuffer(8));
  });

  it('Should get a list of users to delete', () => {
    const mockResponse = [{ id: 1, name: 'Test User' }];

    service.checkUsersToDelete().subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/checkUserToDelete`);
    expect(req.request.method).toBe('GET');
    expect(req.request.withCredentials).toBeTrue();

    req.flush(mockResponse);
  });

  it('Should update the profile', () => {
    const profileData = {
      firstName: null,
      lastName: null,
      allergiesAndMedicalConditions: [],
      phoneNumber: null,
      gender: null,
      appointmentHistory: [],
      emergencyContact: null,
      birthDate: null,
      email: null
    };

    service.updateProfile(profileData).subscribe(response => {
      expect(response).toEqual({ success: true });
    });

    const req = httpMock.expectOne(`${apiUrl}/account/update`);
    expect(req.request.method).toBe('PATCH');
    expect(req.request.withCredentials).toBeTrue();
    expect(req.request.body).toEqual(profileData);

    req.flush({ success: true });
  });
});
