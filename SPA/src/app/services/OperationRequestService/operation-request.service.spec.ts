import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OperationRequestService } from './operation-request.service';
import { OperationRequest } from '../../Domain/OperationRequest';

describe('OperationRequestService', () => {
  let service: OperationRequestService;
  let httpMock: HttpTestingController;
  const apiUrl1 = 'http://localhost:5001/staff';
  const apiUrl = 'http://localhost:5001';

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

  it('should search operation requests with filters', () => {
    const filters = {
      patientName: 'John Doe',
      operationType: 'Surgery',
      priority: 'High',
      status: 'Pending',
    };
    const mockResponse = [{ patientId: '1', doctorId: '2', operationType: 'Surgery', deadline: '2024-12-01', priority: 'High' }];

    service.searchRequests(filters).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(
      `${apiUrl1}/search/requests/John Doe/Surgery/High/Pending/`
    );
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should delete an operation request by ID', () => {
    const operationId = '123';
    service.deleteOperationRequest(operationId).subscribe((response) => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`${apiUrl1}/request/deleteRequest/123`);
    expect(req.request.method).toBe('DELETE');
    req.flush({ success: true });
  });

  it('should add an operation request and return an ID', () => {
    const newRequest: OperationRequest = {
      patientId: '1',
      doctorId: '2',
      operationType: 'Surgery',
      deadline: '2024-12-01',
      priority: 'High',
    };

    service.addOperationRequest(newRequest).subscribe((response) => {
      expect(response).toBe('1');
    });

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest/registerOperationRequest`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newRequest);
    req.flush('1');
  });

  it('should return an empty string on error when adding an operation request', () => {
    const newRequest: OperationRequest = {
      patientId: '1',
      doctorId: '2',
      operationType: 'Surgery',
      deadline: '2024-12-01',
      priority: 'High',
    };

    service.addOperationRequest(newRequest).subscribe((response) => {
      expect(response).toBe('');
    });

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest/registerOperationRequest`);
    req.flush({ message: 'Error occurred' }, { status: 500, statusText: 'Internal Server Error' });
  });

  it('should list all operation requests', () => {
    const mockRequests: OperationRequest[] = [
      { patientId: '1', doctorId: '2', operationType: 'Surgery', deadline: '2024-12-01', priority: 'High' },
      { patientId: '2', doctorId: '3', operationType: 'Checkup', deadline: '2024-11-30', priority: 'Low' },
    ];

    service.listOperationRequests().subscribe((response) => {
      expect(response).toEqual(mockRequests);
    });

    const req = httpMock.expectOne(`${apiUrl}/OperationRequest`);
    expect(req.request.method).toBe('GET');
    req.flush(mockRequests);
  });
});
