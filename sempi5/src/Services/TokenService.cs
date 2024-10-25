using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services;

public class TokenService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TokenService(IConfirmationTokenRepository confirmationRepository, IUserRepository userRepository, 
                        IUnitOfWork unitOfWork, IStaffRepository staffRepository,IPatientRepository patientRepository)
    {
        _confirmationRepository = confirmationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _staffRepository = staffRepository;
        _patientRepository = patientRepository;
    }
    
    public async Task ConfirmStaffAccount(string token)
    {
        var confirmationToken = await _confirmationRepository.GetByIdAndNotUsed(token);
        if (confirmationToken == null)
        {
            throw new Exception("Token not found or already used");
        }
        
        var user = await _userRepository.GetByEmail(confirmationToken.email.ToString());
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        user.Verify();
        
        var staff = await _staffRepository.GetByIdAsync(new StaffId(confirmationToken.IdToAssociate));
        
        confirmationToken.Use();
        staff.AddUser(user);
        
        await _unitOfWork.CommitAsync();
    }
    public async Task confirmPatientAccount(string token)
    {
        var confirmationToken = await _confirmationRepository.GetByIdAndNotUsed(token);
        if (confirmationToken == null)
        {
            throw new Exception("Token not found or already used");
        }
        
        var user = await _userRepository.GetByEmail(confirmationToken.email.ToString());
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        if (user.IsVerified)
        {
            throw new Exception("Email já confirmado");
        }
        
        user.Verify();
        confirmationToken.Use();
       // await _patientRepository.SavePatientAsync(ser);
        
        await _unitOfWork.CommitAsync();
    }
}