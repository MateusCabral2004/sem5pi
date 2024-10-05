using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;

namespace Sempi5.Infrastructure.StaffRepository
{
    public interface IStaffRepository : IRepository<Staff, StaffId>
    {
    }
}
