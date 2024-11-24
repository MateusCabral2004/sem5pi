import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OperationRequestService } from './operation-request.service';
import { OperationRequest } from '../../Domain/OperationRequest';
import { PatientProfile } from '../../Domain/PatientProfile';

describe('OperationRequestService', () => {
  let service: OperationRequestService;
  let httpMock: HttpTestingController;

  const apiUrl1 = 'http://localhost:5001/staff';
  const apiUrl = 'http://localhost:5001/operationRequest';

  const operationRequest: OperationRequest = {
    patient: 'John Doe',
    doctorId: '123',
    operationType: 'Surgery',
    deadline: '2024-12-31',
    priority: 'High',
  };

  const mockPatientProfiles: PatientProfile[] = [
    { id: '1', fullName: 'Jane Smith', email: 'jane.smith@example.com', birthDate: '1990-01-01', phoneNumber: 1234567890, firstName: 'Jane', lastName: 'Smith', gender: 'Female', emergencyContact: 'John Smith' },
  ];

  const mockOperationRequests: OperationRequest[] = [
    { patient: 'John Doe', doctorId: '456', operationType: 'Checkup', deadline: '2024-11-30', priority: 'Low' },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OperationRequestService],
    });

    service = TestBed.inject(OperationRequestService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should search requests with filters', () => {
    const filters = { patientName: 'John', operationType: '', priority: 'High', status: '' };
    service.searchRequests(filters).subscribe();

    const req = httpMock.expectOne(`${apiUrl1}/search/requests/John/null/High/null/`);
    expect(req.request.method).toBe('GET');
  });

  it('should delete an operation request', () => {
    const operationId = '123';
    service.deleteOperationRequest(operationId).subscribe();

    const req = httpMock.expectOne(`${apiUrl1}/request/deleteRequest/123`);
    expect(req.request.method).toBe('DELETE');
  });

  it('should add an operation request', () => {
    service.addOperationRequest(operationRequest).subscribe((response) => {
      expect(response).toBe('');
    });

    const req = httpMock.expectOne(`${apiUrl}/registerOperationRequest`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(operationRequest);
    req.flush('');
  });

  it('should edit operation request deadline', () => {
    service.editOperationRequestDeadline(operationRequest, '2024-12-31', '123').subscribe();

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest/updateOperationRequestDeadline/123/2024-12-31}`);
    expect(req.request.method).toBe('PUT');
  });

  it('should edit operation request priority', () => {
    service.editOperationRequestPriority(operationRequest, 'Medium', '123').subscribe();

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest/updateOperationRequestDeadline/123/Medium}`);
    expect(req.request.method).toBe('PUT');
  });

  it('should list all patient profiles', () => {
    service.listAllPatientProfilesNames().subscribe((profiles) => {
      expect(profiles).toEqual(mockPatientProfiles);
    });

    const req = httpMock.expectOne(`${apiUrl}/Patient`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPatientProfiles);
  });

  it('should list doctor\'s operation requests', () => {
    service.listDoctorsOperationRequests().subscribe((requests) => {
      expect(requests).toEqual(mockOperationRequests);
    });

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest`);
    expect(req.request.method).toBe('GET');
    req.flush(mockOperationRequests);
  });
});
