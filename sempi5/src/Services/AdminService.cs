using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services;

public class AdminService
{
    private readonly IStaffRepository _staffRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly EmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
        
    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository, IUserRepository userRepository,
                        IConfirmationTokenRepository confirmationRepository,IUnitOfWork unitOfWork,
                        EmailService emailService)
    {
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _confirmationRepository = confirmationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }
        
    public async Task RegisterUser(RegisterUserDTO userDTO)
    {
        var user = registerUserDTOtoUser(userDTO);
        var userExists = await _userRepository.GetByEmail(user.Email.ToString());
        if(userExists != null)
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
        if(staff == null)
        {
            throw new ArgumentException("Staff not found");
        }
        if(staff.User.IsVerified)
        {
            throw new ArgumentException("Staff already verified");
        }
        var staffEmail = staff.Person.ContactInfo._email.ToString();
        await _unitOfWork.CommitAsync();
        await _emailService.SendStaffConfirmationEmail(staffEmail, token.Id.ToString());
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
        
        var patient =  await _patientRepository.GetByEmail(email.ToString());

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
    
    public async Task<PatientDTO> ListPatientByMedicalRecordNumber(MedicalRecordNumberDTO medicalRecordNumberDto)
    {
        var medicalRecordNumber = medicalRecordNumberDTOtoMedicalRecordNumber(medicalRecordNumberDto);
        
     //   var patient = await _patientRepository.GetByMedicalRecordNumber(medicalRecordNumber.ToString());
        
     //   if (patient == null)
     //   {
      //      throw new ArgumentException("Patient not found.");
    //    }
   //     
    //    return patientToPatientDto(patient);

    return null;
    }
    
    private SystemUser userDTOtoUser(SystemUserDTO user)
    {
        var email = new Email(user.email);
        return new SystemUser(email, user.role);
    }
    
    private SystemUser registerUserDTOtoUser(RegisterUserDTO user)
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
       // return new MedicalRecordNumber(medicalRecordNumber.medicalrecordnumber);
       return null;
    }
    
    private PatientDTO patientToPatientDto(Patient patient)
    {
        return new PatientDTO
        {
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
        return new Patient(null, new Person(new Name(patientDTO.firstName), new Name(patientDTO.lastName), new ContactInfo(new Email(patientDTO.email),
            new PhoneNumber(patientDTO.phoneNumber))), DateTime.Parse(patientDTO.birthDate), patientDTO.gender, patientDTO.allergiesAndMedicalConditions,
            patientDTO.emergencyContact, patientDTO.appointmentHistory);
    }
    
    private async Task<ConfirmationToken> RegisterToken(ConfirmationToken confirmationToken)
    {
        var token = await _confirmationRepository.GetByEmail(confirmationToken.email.ToString());

        if (token == null)
        {
            return await _confirmationRepository.AddAsync(confirmationToken);
        }
        
        if(!token.Id.Equals(confirmationToken.Id))
        {
            throw new ArgumentException("Account already waiting for verification with a different staff profile. If you made a mistake, please contact support.");
        }
        
        token.ResetExpiryDate();
        
        return token;

    }
    
}