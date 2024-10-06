using Sempi5.Domain.Patient;
using Sempi5.Domain.Staff;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Services
{
    public class LoginService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;

        public LoginService(IStaffRepository staffRepository, IUserRepository userRepository, IPatientRepository patientRepository)
        {
            _staffRepository = staffRepository;
            _userRepository= userRepository;
            _patientRepository = patientRepository;
        }

        public async Task<Staff> getStaffFromEmail(string email)
        {
            var staff = await _staffRepository.GetByEmail(email);
            return staff;
        }

        public async Task<SystemUser> getUserFromEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user;
        }

        public async Task<Patient> getPatientFromEmail(string email)
        {
            var patient = await _patientRepository.GetByEmail(email);
            return patient;
        }
    }
}
        