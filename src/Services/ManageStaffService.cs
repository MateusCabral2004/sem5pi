using Sempi5.Domain.Staff;
using Sempi5.Domain.Staff.DTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services
{
    public class ManageStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        
        public ManageStaffService(IStaffRepository staffRepository, IUserRepository userRepository)
        {
            _staffRepository = staffRepository;
            _userRepository = userRepository;
        }

        public Staff AddStaff(StaffDTO staffDTO)
        {
            var staff = DTOtoStaff(staffDTO);
            _staffRepository.AddAsync(staff);
            _userRepository.AddAsync(staff.User);
            return staff;
        }

        public Task<List<Staff>> GetAll(){
            return _staffRepository.GetAllAsync();
        }
        
        public Staff DTOtoStaff(StaffDTO staffDTO)
        {
            var staff = new Staff
            {
                FirstName = staffDTO.FirstName,
                LastName = staffDTO.LastName,
                FullName = staffDTO.FullName,
                Id = new LicenseNumber(staffDTO.LicenseNumber),
                Specialization = staffDTO.Specialization,
                ContactInfo = staffDTO.ContactInfo,
                //AvailabilitySlots = staffDTO.AvailabilitySlots,
                Password = staffDTO.Password,
                User = new SystemUser(staffDTO.Email, staffDTO.Role)
            };
            return staff;
        }

        public static StaffDTO StaffToDTO(Staff staff) =>
        new StaffDTO
        {
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            FullName = staff.FullName,
            LicenseNumber = staff.Id.Value,
            Specialization = staff.Specialization,
            ContactInfo = staff.ContactInfo,
            AvailabilitySlots = staff.AvailabilitySlots,
            Password = staff.Password,
            Email = staff.User?.Email,
            Role = staff.User?.Role  
        };
    }
}
    
