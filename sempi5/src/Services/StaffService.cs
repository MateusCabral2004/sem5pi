using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services
{
    public class StaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        
        public StaffService(IStaffRepository staffRepository, IUserRepository userRepository)
        {
            _staffRepository = staffRepository;
            _userRepository = userRepository;
        }
    }
}
    
