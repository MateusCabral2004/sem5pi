using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffRepository : BaseRepository<Staff,StaffId>, IStaffRepository
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

            Email emailObj = new Email(email);
            
            var staff = await Task.Run(() => context.StaffMembers
                .Include(u => u.User)
                .Include(u => u.Person)
                .FirstOrDefault(u => u.User.Email.Equals(emailObj)));

            return staff;
        }
        
        public async Task<Staff> GetByIdAsync(StaffId id)
        {
            //return await this._context.Categories.FindAsync(id);
            var staff = await Task.Run(() => context.StaffMembers
                .Include(x => x.User)
                .Include(x => x.Person)
                .Where(x => id.Equals(x.Id)).FirstOrDefaultAsync()
                );
            
            return staff;
        }
        
    }
}
