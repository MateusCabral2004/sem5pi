import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StaffService } from './staff.service';
import { Staff } from '../../Domain/Staff';
import { CreateStaff } from '../../Domain/CreateStaff';

describe('StaffService', () => {
  let service: StaffService;
  let httpMock: HttpTestingController;

  const apiUrl = 'http://localhost:5001';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StaffService],
    });

    service = TestBed.inject(StaffService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('listAllStaffProfiles', () => {
    it('should return a list of staff profiles', () => {
      const mockStaffList: Staff[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', phoneNumber: 123456789, specialization: 'Doctor' },
        { id: '2', fullName: 'Jane Doe', email: 'jane.doe@example.com', phoneNumber: 987654321, specialization: 'Nurse' },
      ];

      service.listAllStaffProfiles().subscribe(response => {
        expect(response).toEqual(mockStaffList);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff`);
      expect(req.request.method).toBe('GET');
      req.flush(mockStaffList);
    });
  });

  describe('filterStaffProfilesByName', () => {
    it('should return a list of staff profiles filtered by name', () => {
      const mockStaffList: Staff[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', phoneNumber: 123456789, specialization: 'Doctor' },
      ];

      service.filterStaffProfilesByName('John').subscribe(response => {
        expect(response).toEqual(mockStaffList);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff/by-name/John`);
      expect(req.request.method).toBe('GET');
      req.flush(mockStaffList);
    });
  });

  describe('filterStaffProfilesByEmail', () => {
    it('should return a staff profile filtered by email', () => {
      const mockStaff: Staff = { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', phoneNumber: 123456789, specialization: 'Doctor' };

      service.filterStaffProfilesByEmail('john.doe@example.com').subscribe(response => {
        expect(response).toEqual(mockStaff);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff/by-email/john.doe@example.com`);
      expect(req.request.method).toBe('GET');
      req.flush(mockStaff);
    });
  });

  describe('filterStaffProfilesBySpecialization', () => {
    it('should return a list of staff profiles filtered by specialization', () => {
      const mockStaffList: Staff[] = [
        { id: '1', fullName: 'John Doe', email: 'john.doe@example.com', phoneNumber: 123456789, specialization: 'Doctor' },
      ];

      service.filterStaffProfilesBySpecialization('Doctor').subscribe(response => {
        expect(response).toEqual(mockStaffList);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff/by-specialization/Doctor`);
      expect(req.request.method).toBe('GET');
      req.flush(mockStaffList);
    });
  });

  describe('deleteStaffProfile', () => {
    it('should delete a staff profile by ID', () => {
      const staffId = '1';
      const mockResponse = 'Staff deleted successfully';

      service.deleteStaffProfile(staffId).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff/${staffId}`);
      expect(req.request.method).toBe('DELETE');
      req.flush(mockResponse);
    });
  });

  describe('updateStaffProfile', () => {
    it('should update a staff profile', () => {
      const updatedStaff: Staff = {
        id: '1',
        fullName: 'John Doe Updated',
        email: 'john.doe.updated@example.com',
        phoneNumber: 987654321,
        specialization: 'Doctor',
      };
      const mockResponse = 'Staff profile updated successfully';

      service.updateStaffProfile(updatedStaff).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff`);
      expect(req.request.method).toBe('PATCH');
      expect(req.request.body).toEqual(updatedStaff);
      req.flush(mockResponse);
    });
  });

  describe('createStaffProfile', () => {
    it('should create a new staff profile', () => {
      const newStaff: CreateStaff = {
        FirstName: 'John',
        LastName: 'Doe',
        LicenseNumber: 12345,
        Specialization: 'Doctor',
        Email: 'john.doe@example.com',
        PhoneNumber: 123456789,
      };
      const mockResponse = 'Staff profile created successfully';

      service.createStaffProfile(newStaff).subscribe(response => {
        expect(response).toBe(mockResponse);
      });

      const req = httpMock.expectOne(`${apiUrl}/Staff`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(newStaff);
      req.flush(mockResponse);
    });
  });
});
