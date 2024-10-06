using Sempi5.Domain.Staff;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private readonly DBContext context;

        public UserRepository(DBContext context)
        {
            this.context = context;
        }

        public async Task<SystemUser> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await Task.Run(() => context.Users
                .FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower())));

            return user;
        }

        public async Task<SystemUser> AddAsync(SystemUser user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return await Task.FromResult(user);
        }
    }
}