using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.UserAggregate;

namespace Sempi5.Infrastructure.UserRepository
{
    public class UserRepository : BaseRepository<SystemUser, SystemUserId>, IUserRepository
    {
        private readonly DBContext context;

        public UserRepository(DBContext context) : base(context.Users)
        {
            this.context = context;
        }

        public async Task<SystemUser> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(new Email(email.ToLower())));

            return user;
        }

        public async Task<SystemUser> GetByEmailAndItsActivated(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await context.Users.FirstOrDefaultAsync(u =>
                u.Email.Equals(new Email(email.ToLower())) && u.IsVerified);

            return user;
        }

        public async Task RemoveAsync(SystemUser user)
        {
            context.Users.Remove(user);
        }
    }
}