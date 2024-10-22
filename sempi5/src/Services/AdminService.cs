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
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUnitOfWork _unitOfWork;
        
    public AdminService(IStaffRepository staffRepository, IPatientRepository patientRepository, IUserRepository userRepository,
                        IConfirmationTokenRepository confirmationRepository,IUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _confirmationRepository = confirmationRepository;
        _unitOfWork = unitOfWork;
    }
        
    public async Task RegisterUser(SystemUserDTO userDTO)
    {
        var user = userDTOtoUser(userDTO);
        await _userRepository.AddAsync(user);
        var token = await _confirmationRepository.AddAsync(new ConfirmationToken(userDTO.email));
        Console.WriteLine("Token: " + token.Id);
        
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
}