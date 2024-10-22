using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;
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
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUnitOfWork _unitOfWork;
        
    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository, IUserRepository userRepository,
                        IConfirmationTokenRepository confirmationRepository,IUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _userRepository = userRepository;
        _confirmationRepository = confirmationRepository;
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }
        
    public async Task RegisterUser(SystemUserDTO userDTO)
    {
        var user = userDTOtoUser(userDTO);
        await _userRepository.AddAsync(user);
        var token = await _confirmationRepository.AddAsync(new ConfirmationToken(userDTO.email));
        Console.WriteLine("Token: " + token.Id);
        Console.WriteLine("Confirmation Link:" + "http://localhost:5001/confirmToken/" + token.Id);
        
        await _unitOfWork.CommitAsync();
        //generate email verification token
        
        //send email verification
    }
    
    public async Task<IEnumerable<Patient>> ListPatientProfiles()
    {
        return await _patientRepository.GetAllAsync();
    }

    public async Task<Patient> ListPatientByEmail(string email)
    {
        return await _patientRepository.GetByEmail(email);
    }
    
    public async Task<Patient> ListPatientByName(string name)
    {
        return await _patientRepository.GetByName(name);
    }
    
    private SystemUser userDTOtoUser(SystemUserDTO user)
    {
        return new SystemUser(user.email, user.role);
    }

    public async Task CreatePatientProfile(PatientDTO patientDTO)
    {
        var patient = patientDTOToPatient(patientDTO);
        await _patientRepository.AddAsync(patient);

        await _unitOfWork.CommitAsync();

    }

    private Patient patientDTOToPatient(PatientDTO patient)
    {
        return new Patient(null, patient.Person, patient.BirthDate, patient.Gender,
            patient.AllergiesAndMedicalConditions, patient.EmergencyContact, patient.AppointmentHistory);
    }

    
    
}