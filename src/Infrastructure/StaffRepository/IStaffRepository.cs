using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;

namespace Sempi5.Infrastructure.StaffRepository
{
    public interface IStaffRepository
    {
        public Task<Staff> GetByEmail(string email);
        
        public Task<Staff> AddAsync(Staff staff);

        public Task<List<Staff>> GetAllAsync();
    }
}
