using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Staff;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffRepository : IStaffRepository
    {

        private readonly DBContext context;

        public StaffRepository(DBContext context)
        {
            this.context = context;
        }

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

        public async Task<List<Staff>> GetAllAsync()
        {
            return await context.StaffMembers.Include(u => u.User).ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(string licenseNumber)
        {
            return await context.StaffMembers.Include(u => u.User).FirstOrDefaultAsync(s => s.LicenseNumber == licenseNumber);
        }

        public async Task<IEnumerable<Staff>> GetByIdsAsync(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public void Remove(Staff staff)
        {
            context.StaffMembers.Remove(staff);
            context.SaveChanges();
        }

        public Task<Staff> AddAsync(Staff staff)
        {
            context.StaffMembers.Add(staff);
            context.SaveChanges();
            return Task.FromResult(staff);
        }

    }
}
