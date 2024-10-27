using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.UserAggregate
{
    public interface IUserRepository : IRepository<SystemUser,SystemUserId>
    {
        public Task<SystemUser> GetByEmail(string email);
        public Task<SystemUser> GetByEmailAndItsActivated(string email);
    }
}
