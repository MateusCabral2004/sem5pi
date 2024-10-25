using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.PersonRepository;
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
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository,
        IUserRepository userRepository,
        IConfirmationTokenRepository confirmationRepository, IUnitOfWork unitOfWork, EmailService emailService,
        IPersonRepository personRepository, ISpecializationRepository specializationRepository)
    {
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _confirmationRepository = confirmationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _personRepository = personRepository;
        _specializationRepository = specializationRepository;
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

    public async Task<PatientDTO> ListPatientByName(NameDTO nameDto)
    {
        var name = nameDTOtoName(nameDto);

        var patient = await _patientRepository.GetByName(name.ToString());

        if (patient == null)
        {
            throw new ArgumentException("Patient not found");
        }

        return patientToPatientDto(patient);
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

    public async Task<List<PatientDTO>> ListPatientByDateOfBirth(DateDTO dateDto)
    {
        var date = dateDTOtoDate(dateDto);

        var patients = await _patientRepository.GetByDateOfBirth(date);

        var patientDtoList = buildPatientDtoList(patients);

        if (patientDtoList.Count == 0)
        {
            throw new ArgumentException("No patients found.");
        }

        return patientDtoList;
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

    public async Task<PatientRecordDTO> EditPatientRecord(EditPatientRecordDTO editPatientRecord)
    {
        var patient = await _patientRepository.GetByPatientIdWithActivatedMedicalRecord(editPatientRecord.Id);

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        patient.AppointmentHistory.Add(editPatientRecord.recordToAdd);

        await _patientRepository.SavePatientAsync(patient);

        return patientToPatientRecordDto(patient);
    }

    public async Task<PatientRecordDTO> DeletePatientRecord(PatientIdDto patientId)
    {
        var patient = await _patientRepository.GetByPatientIdWithActivatedMedicalRecord(patientId.Id);

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        patient.MedicalRecordStatus = MedicalRecordStatusEnum.DEACTIVATED;

        await _patientRepository.SavePatientAsync(patient);

        return patientToPatientRecordDto(patient);
    }


    private SystemUser userDTOtoUser(SystemUserDTO user)
    {
        var email = new Email(user.email);
        return new SystemUser(email, user.role);
    }

    public async Task CreatePatientProfile(PatientDTO patientDTO)
    {
        var patient = patientDTOToPatient(patientDTO);
        await _patientRepository.AddAsync(patient);

        await _unitOfWork.CommitAsync();
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

    private Patient patientDTOToPatient(PatientDTO patientDTO)
    {
        return new Patient(null, new Person(new Name(patientDTO.firstName), new Name(patientDTO.lastName),
                new ContactInfo(new Email(patientDTO.email),
                    new PhoneNumber(patientDTO.phoneNumber ?? 0))), DateTime.Parse(patientDTO.birthDate),
            patientDTO.gender,
            patientDTO.allergiesAndMedicalConditions,
            patientDTO.emergencyContact, patientDTO.appointmentHistory);
    }

    public async Task EditPatientProfile(PatientDTO editPatientDto, string email)
    {
        var patient = await _patientRepository.GetByEmail(email);

        var originalEmail = patient.Person.ContactInfo._email;

        if (editPatientDto.allergiesAndMedicalConditions != null)
        {
            patient.AllergiesAndMedicalConditions = editPatientDto.allergiesAndMedicalConditions;
        }

        if (editPatientDto.appointmentHistory != null)
        {
            patient.AppointmentHistory = editPatientDto.appointmentHistory;
        }

        if (editPatientDto.phoneNumber == -1)
        {
            patient.Person.ContactInfo._phoneNumber = new PhoneNumber(editPatientDto.phoneNumber ?? 0);
        }

        if (editPatientDto.email != null)
        {
            patient.Person.ContactInfo._email = new Email(editPatientDto.firstName);
        }

        if (editPatientDto.fullName != null)
        {
            patient.Person.FullName._name = editPatientDto.fullName;
        }

        await _patientRepository.AddAsync(patient);
        await _unitOfWork.CommitAsync();


        //TODO TEM QUE ENVIAR EMAIL PARA O ANTIGO EMAIL
        //TODO LOGS
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
    
    public async Task EditPatientProfile2(PatientDTO editPatientDto)
    {
        if (editPatientDto.email == null)
        {
            throw new ArgumentException("The email address can´t be null");
        }
        var patient =await _patientRepository.GetByEmail(editPatientDto.email);

        var originalEmail = patient.Person.ContactInfo._email;

        var originalPhoneNumber = patient.Person.ContactInfo._phoneNumber;
        
        if (editPatientDto.allergiesAndMedicalConditions != null)
        {
            patient.AllergiesAndMedicalConditions = editPatientDto.allergiesAndMedicalConditions;
        }

        if (editPatientDto.appointmentHistory != null)
        {
            patient.AppointmentHistory = editPatientDto.appointmentHistory;
        }

        if (editPatientDto.phoneNumber == -1)
        {
            patient.Person.ContactInfo._phoneNumber = new PhoneNumber(editPatientDto.phoneNumber??0);
        }

        if (editPatientDto.email != null)
        {
            patient.Person.ContactInfo._email = new Email(editPatientDto.firstName);
        }

        if (editPatientDto.fullName != null)
        {
            patient.Person.FullName._name = editPatientDto.fullName;
        }

        await _patientRepository.AddAsync(patient);
        await _unitOfWork.CommitAsync();

        if (originalEmail.ToString() != editPatientDto.email)
        {
           await _emailService.SendPatientUpdatingEmail_EmailAltered(originalEmail.ToString(), editPatientDto.email);
        }

        if (originalPhoneNumber.phoneNumber() != editPatientDto.phoneNumber)
        {
           await _emailService.SendPatientUpdatingEmail_PhoneNumberAltered(originalEmail.ToString(),
                editPatientDto.phoneNumber.ToString());
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
    
    public async Task DeletePatientProfile2(PatientDTO patientDto)
    {
        if (patientDto.email==null)
        {
            throw new ArgumentException("The email address can´t be null");
        }
        var patient = _patientRepository.GetByEmail(patientDto.email);

        patient.Result.Person = new Person(new Name("\"anonymous\";"), new Name("\"anonymous\";"),
            new ContactInfo(new Email("\"anonymous\";"), new PhoneNumber(int.Parse("\"anonymous\";"))));
        patient.Result.BirthDate = DateTime.Parse("\"anonymous\";");
        patient.Result.EmergencyContact = "\"anonymous\";";
        patient.Result.AllergiesAndMedicalConditions = new List<string>{"\"anonymous\";"};
        
        
        await _unitOfWork.CommitAsync();
    }

    
}