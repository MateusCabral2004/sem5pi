using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffRepository : BaseRepository<Staff, StaffId>, IStaffRepository
    {
    
        public StaffRepository(DBContext context):base(context.StaffMembers)
        {
           
        }


    }
}