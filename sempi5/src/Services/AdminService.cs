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
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly EmailService _emailService;
    private readonly IPersonRepository _personRepository;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IOperationTypeRepository _operationTypeRepository;
    private readonly IRequiredStaffRepository _requiredStaffRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository,
        IUserRepository userRepository,
        IConfirmationTokenRepository confirmationRepository, IUnitOfWork unitOfWork, EmailService emailService,
        IPersonRepository personRepository, ISpecializationRepository specializationRepository,
        IOperationTypeRepository operationTypeRepository, IRequiredStaffRepository requiredStaffRepository)
    {
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _confirmationRepository = confirmationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _personRepository = personRepository;
        _specializationRepository = specializationRepository;
        _operationTypeRepository = operationTypeRepository;
        _requiredStaffRepository = requiredStaffRepository;
    }

    public async Task RegisterUser(RegisterUserDTO userDTO)
    {
        var user = registerUserDTOtoUser(userDTO);
        var userExists = await _userRepository.GetByEmail(user.Email.ToString());
        if (userExists != null)
        {
            if (!userExists.IsVerified)
            {
                Console.WriteLine("User already exists, but not verified");
                userExists.Role = userDTO.role;
            }
            else
            {
                throw new ArgumentException("User already exists");
            }
        }
        else
        {
            await _userRepository.AddAsync(user);
        }

        var email = new Email(userDTO.email);
        var confirmationToken = new ConfirmationToken(email, userDTO.staffOrStaffId);

        var token = await RegisterToken(confirmationToken);
        var staff = await _staffRepository.GetByIdAsync(new StaffId(userDTO.staffOrStaffId));
        if (staff == null)
        {
            throw new ArgumentException("Staff not found");
        }

        if (staff.User.IsVerified)
        {
            throw new ArgumentException("Staff already verified");
        }

        var staffEmail = staff.Person.ContactInfo._email.ToString();
        await _unitOfWork.CommitAsync();
        await _emailService.SendStaffConfirmationEmail(staffEmail, token.Id.ToString());
    }

    private SystemUser registerUserDTOtoUser(RegisterUserDTO user)
    {
        var email = new Email(user.email);
        return new SystemUser(email, user.role);
    }

    private async Task<ConfirmationToken> RegisterToken(ConfirmationToken confirmationToken)
    {
        var token = await _confirmationRepository.GetByEmail(confirmationToken.email.ToString());

        if (token == null)
        {
            return await _confirmationRepository.AddAsync(confirmationToken);
        }

        if (!token.Id.Equals(confirmationToken.Id))
        {
            throw new ArgumentException(
                "Account already waiting for verification with a different staff profile. If you made a mistake, please contact support.");
        }

        token.ResetExpiryDate();

        return token;
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