using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services;

public class AdminService
{
    private readonly IStaffRepository _staffRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
        
    public AdminService(IStaffRepository staffRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
        
    public async Task RegisterUser(SystemUserDTO userDTO)
    {
        var user = userDTOtoUser(userDTO);
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        //generate email verification token
        //send email verification
    }
    
    private SystemUser userDTOtoUser(SystemUserDTO user)
    {
        return new SystemUser(user.email, user.role);
    }
}