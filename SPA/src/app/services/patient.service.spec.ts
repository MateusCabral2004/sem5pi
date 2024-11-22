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
      providers: [PatientService],
    });

    service = TestBed.inject(PatientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should make a POST request with the correct parameters', () => {
    const mockNumber = 12345;
    const mockResponse = new ArrayBuffer(10);

    service.registerNumber(mockNumber).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne((request) => {
      return request.method === 'POST' &&
        request.url === `${apiUrl}/register` &&
        request.params.has('number') &&
        request.params.get('number') === String(mockNumber);
    });

    expect(req.request.responseType).toBe('arraybuffer');
    expect(req.request.withCredentials).toBeTrue();
    req.flush(mockResponse);
  });
});
