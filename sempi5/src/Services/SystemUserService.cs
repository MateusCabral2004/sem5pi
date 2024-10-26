using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services;

public class SystemUserService
{
    
    private readonly IStaffRepository _staffRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<RegisterUserDTO> RegisterUser(RegisterUserDTO userDTO)
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
        var registerUserResponseDto = new RegisterUserDTO
        {
            email = staffEmail,
            role = userDTO.role,
            staffOrStaffId = token.Id.ToString()
        };
        return registerUserResponseDto;
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
    
    
}