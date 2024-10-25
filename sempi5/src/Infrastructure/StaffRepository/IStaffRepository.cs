using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate;

namespace Sempi5.Infrastructure.StaffRepository
{
    public interface IStaffRepository : IRepository<Staff, StaffId>
    {
        public Task<Staff> GetByEmail(string email);
        
        public Task<Staff?> GetByLicenseNumber(LicenseNumber licenseNumber);
    }
}
