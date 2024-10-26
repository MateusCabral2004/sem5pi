using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.OperationTypeAggregate.DTOs;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.RequiredStaffAggregate.DTOs;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.OperationTypeRepository;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.PersonRepository;
using Sempi5.Infrastructure.RequiredStaffRepository;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.SpecializationRepository;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;
using Sempi5.Services;

namespace Sempi5.Services;

public class AdminService
{
    private readonly IStaffRepository _staffRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly EmailService _emailService;
    private readonly IPersonRepository _personRepository;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository, 
        IUnitOfWork unitOfWork, EmailService emailService,
        IPersonRepository personRepository, ISpecializationRepository specializationRepository)
    {
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _personRepository = personRepository;
        _specializationRepository = specializationRepository;
    }
    
    public async Task<PatientDTO> ListPatientByEmail(EmailDTO emailDto)
    {
        var email = emailDTOtoEmail(emailDto);

        var patient = await _patientRepository.GetByEmail(email.ToString());

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        return patientToPatientDto(patient);
    }
    

    public async Task<PatientDTO> ListPatientByMedicalRecordNumber(PatientIdDto patientId)
    {
        var patient = await _patientRepository.GetByPatientId(patientId.Id);

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        return patientToPatientDto(patient);
    }

    public async Task<PatientRecordDTO> GetPatientRecordByPatientId(PatientIdDto patientId)
    {
        var patient = await _patientRepository.GetByPatientId(patientId.Id);

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        return patientToPatientRecordDto(patient);
    }
    

    private Email emailDTOtoEmail(EmailDTO email)
    {
        return new Email(email.email);
    }

    private DateTime dateDTOtoDate(DateDTO date)
    {
        return new DateTime(date.year, date.month, date.day);
    }

    private Name nameDTOtoName(NameDTO name)
    {
        return new Name(name.name);
    }

    private MedicalRecordNumber medicalRecordNumberDTOtoMedicalRecordNumber(MedicalRecordNumberDTO medicalRecordNumber)
    {
        return new MedicalRecordNumber(medicalRecordNumber.ToString());
    }

    private PatientRecordDTO patientToPatientRecordDto(Patient patient)
    {
        return new PatientRecordDTO
        {
            id = patient.Id.AsString(),
            name = patient.Person.FullName.ToString(),
            appointments = patient.AppointmentHistory.Select(a => a.ToString()).ToList()
        };
    }


    private PatientDTO patientToPatientDto(Patient patient)
    {
        return new PatientDTO
        {
            patientId = patient.Id.AsString(),
            fullName = patient.Person.FullName.ToString(),
            email = patient.Person.ContactInfo.email().ToString(),
            birthDate = patient.BirthDate.ToString("MM/dd/yyyy")
        };
    }

    private List<PatientDTO> buildPatientDtoList(IEnumerable<Patient> patients)
    {
        List<PatientDTO> patientDtoList = new List<PatientDTO>();

        foreach (var patient in patients)
        {
            patientDtoList.Add(patientToPatientDto(patient));
        }

        return patientDtoList;
    }
    

    public async Task CreateStaffProfile(StaffDTO staffDTO)
    {
        var staff = await StaffDtoToStaff(staffDTO);

        await _staffRepository.AddAsync(staff);

        await _unitOfWork.CommitAsync();
    }

    public async Task<Staff> StaffDtoToStaff(StaffDTO staffDTO)
    {
        LicenseNumber licenseNumber = new LicenseNumber(staffDTO.LicenseNumber);

        await VerifyLicenseNumberAvailability(licenseNumber);

        var person = await CreatePerson(staffDTO.FirstName, staffDTO.LastName, staffDTO.Email, staffDTO.PhoneNumber);

        var specialization = await CreateSpecialization(staffDTO.Specialization);

        return new Staff(licenseNumber, person, specialization);
    }

    public async Task<Specialization> CreateSpecialization(string specializationName)
    {
        var specialiName = new SpecializationName(specializationName);

        Specialization specialization = new Specialization(specialiName);

        var searchedSpecialization = await _specializationRepository.GetBySpecializationName(specialization);

        if (searchedSpecialization != null)
        {
            return searchedSpecialization;
        }

        return specialization;
    }

    public async Task<Person> CreatePerson(string firstName, string lastName, string emailString, int phoneNumberInt)
    {
        var phoneNumber = new PhoneNumber(phoneNumberInt);

        var email = new Email(emailString);

        await VerifyPhoneNumberAvailability(phoneNumber);

        await VerifyEmailAvailability(email);

        var contactInfo = new ContactInfo(email, phoneNumber);
        var person = new Person(new Name(firstName), new Name(lastName), contactInfo);

        return person;
    }

    public async Task VerifyPhoneNumberAvailability(PhoneNumber phoneNumber)
    {
        var personByPhoneNumber = await _personRepository.GetPersonByPhoneNumber(phoneNumber);

        if (personByPhoneNumber != null)
        {
            throw new ArgumentException("Phone Number already in use.");
        }
    }

    public async Task VerifyEmailAvailability(Email email)
    {
        var personByEmail = await _personRepository.GetPersonByEmail(email);

        if (personByEmail != null)
        {
            throw new ArgumentException("Email already in use.");
        }
    }

    public async Task VerifyLicenseNumberAvailability(LicenseNumber licenseNumber)
    {
        var staffByLicenseNumber = await _staffRepository.GetByLicenseNumber(licenseNumber);

        if (staffByLicenseNumber != null)
        {
            throw new ArgumentException("License Number already in use.");
        }
    }

    public async Task<StaffDTO> EditStaffProfile(EditStaffDTO editStaffDto)
    {

        var staff = await _staffRepository.GetActiveStaffById(new StaffId(editStaffDto.Id));
        
        if (staff == null)
        {
            throw new ArgumentException("Staff not found.");
        }

        if (editStaffDto.email != null)
        {
            var email = new Email(editStaffDto.email);

            await VerifyEmailAvailability(email);

            staff.Person.ContactInfo._email = email;
        }


        if (editStaffDto.phoneNumber > 0 && editStaffDto.phoneNumber != null)
        {
            var phoneNumber = new PhoneNumber(editStaffDto.phoneNumber);

            await VerifyPhoneNumberAvailability(phoneNumber);

            staff.Person.ContactInfo._phoneNumber = phoneNumber;
        }

        if (editStaffDto.specialization != null)
        {
            staff.Specialization = await CreateSpecialization(editStaffDto.specialization);
        }

        await _unitOfWork.CommitAsync();

        return await StaffToStaffDto(staff);
    }

    public async Task<StaffDTO> StaffToStaffDto(Staff staff)
    {
        return new StaffDTO
        {
            FirstName = staff.Person.FirstName.ToString(),
            LastName = staff.Person.LastName.ToString(),
            LicenseNumber = staff.LicenseNumber.licenseNumber(),
            Email = staff.Person.ContactInfo._email.ToString(),
            PhoneNumber = staff.Person.ContactInfo._phoneNumber.phoneNumber(),
            Specialization = staff.Specialization.specializationName.ToString()
        };
    }
    
    
}