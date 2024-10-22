using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services;

public class TokenService
{
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TokenService(IConfirmationTokenRepository confirmationRepository, IUserRepository userRepository, 
                        IUnitOfWork unitOfWork)
    {
        _confirmationRepository = confirmationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task ConfirmAccount(string token)
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
        
        confirmationToken.Use();
        
        await _unitOfWork.CommitAsync();
    }
}