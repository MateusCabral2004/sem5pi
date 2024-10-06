using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.UserRepository
{
    public interface IUserRepository
    {
        public Task<SystemUser> GetByEmail(string email);
        public Task<SystemUser> AddAsync(SystemUser user);
    }
}
