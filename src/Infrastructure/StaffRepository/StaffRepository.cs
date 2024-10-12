using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffRepository : BaseRepository<Staff,LicenseNumber>, IStaffRepository
    {

        private readonly DBContext context;

        public StaffRepository(DBContext context) : base(context.StaffMembers)
        { this.context = context; }

        public async Task<Staff> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var staff = await Task.Run(() => context.StaffMembers
                .Include(u => u.User)
                .FirstOrDefault(u => u.User.Email.ToLower().Equals(email.ToLower())));

            return staff;
        }
    }
}
