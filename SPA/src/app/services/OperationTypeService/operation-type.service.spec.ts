import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OperationTypeService } from './operation-type.service';
import { OperationType } from '../../Domain/OperationType';
import { RequiredStaff } from '../../Domain/RequiredStaff';

describe('OperationTypeService', () => {
  let service: OperationTypeService;
  let httpMock: HttpTestingController;

  const apiUrl = 'http://localhost:5001/operationType';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OperationTypeService],
    });

    service = TestBed.inject(OperationTypeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('addOperationType', () => {
    it('should add a new operation type with required staff', () => {
      const mockOperation: OperationType = {
        operationName: 'Test Operation',
        requiredStaff: [{numberOfStaff: 2, specialization: 'Nurse'}],
        setupDuration: '15',
        surgeryDuration: '60',
        cleaningDuration: '30',
      };

      const mockResponse = 'Operation added successfully';

      service.addOperationType(mockOperation).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/addNewOperationType`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(mockOperation);
      expect(req.request.withCredentials).toBeTrue();

      req.flush(mockResponse);
    });
  });

  describe('deleteOperationType', () => {
    it('should delete an operation type by name', () => {
      const mockOperation: OperationType = {
        operationName: 'Test Operation',
        requiredStaff: [],
        setupDuration: '15',
        surgeryDuration: '60',
        cleaningDuration: '30',
      };

      const mockResponse = 'Operation deleted successfully';

      service.deleteOperationType(mockOperation).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/deleteOperationType/Test%20Operation`);
      expect(req.request.method).toBe('DELETE');
      req.flush(mockResponse);
    });
  });

  describe('addRequiredStaff', () => {
    it('should add required staff to an operation', () => {
      const mockOperation: OperationType = {operationName: 'Test Operation', requiredStaff: [], setupDuration: '15', surgeryDuration: '60', cleaningDuration: '30'};
      const mockStaff: RequiredStaff = {numberOfStaff: 1, specialization: 'Surgeon'};
      const mockResponse = 'Staff added successfully';

      service.addRequiredStaff(mockOperation, mockStaff).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/editOperationType/requiredStaff/add/Test%20Operation`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(mockStaff);
      req.flush(mockResponse);
    });
  });

  describe('removeRequiredStaff', () => {
    it('should remove required staff from an operation', () => {
      const mockOperation: OperationType = {operationName: 'Test Operation', requiredStaff: [], setupDuration: '15', surgeryDuration: '60', cleaningDuration: '30'};
      const mockStaff: RequiredStaff = {numberOfStaff: 1, specialization: 'Surgeon'};
      const mockResponse = 'Staff removed successfully';

      service.removeRequiredStaff(mockOperation, mockStaff).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/editOperationType/requiredStaff/remove/Test%20Operation`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toBe(JSON.stringify(mockStaff.specialization));
      req.flush(mockResponse);
    });
  });

  describe('updateDuration', () => {
    it('should update operation duration', () => {
      const mockResponse = 'Duration updated successfully';

      service.updateDuration('Test Operation', '00120', 'surgeryDuration').subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/editOperationType/duration/surgeryDuration/Test%20Operation`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toBe(JSON.stringify('120'));
      req.flush(mockResponse);
    });
  });

  describe('listOperationTypes', () => {
    it('should list all operation types', () => {
      const mockOperations: OperationType[] = [
        {
          operationName: 'Operation 1',
          requiredStaff: [{numberOfStaff: 2, specialization: 'Nurse'}],
          setupDuration: '15',
          surgeryDuration: '45',
          cleaningDuration: '20',
        },
        {
          operationName: 'Operation 2',
          requiredStaff: [{numberOfStaff: 1, specialization: 'Surgeon'}],
          setupDuration: '10',
          surgeryDuration: '30',
          cleaningDuration: '15',
        },
      ];

      service.listOperationTypes().subscribe(response => {
        expect(response).toEqual(mockOperations);
      });

      const req = httpMock.expectOne(`${apiUrl}/listOperationType`);
      expect(req.request.method).toBe('GET');
      req.flush(mockOperations);
    });
  });

  describe('filterOperationTypes', () => {
    it('should filter operation types by criteria', () => {
      const mockFilteredOperations: OperationType[] = [
        {
          operationName: 'Operation 1',
          requiredStaff: [{numberOfStaff: 2, specialization: 'Nurse'}],
          setupDuration: '15',
          surgeryDuration: '45',
          cleaningDuration: '20',
        },
      ];

      service.filterOperationTypes('Specialization', 'Nurse').subscribe(response => {
        expect(response).toEqual(mockFilteredOperations);
      });

      const req = httpMock.expectOne(`${apiUrl}/listOperationTypeBySpecialization/Nurse`);
      expect(req.request.method).toBe('GET');
      req.flush(mockFilteredOperations);
    });
  });
});
