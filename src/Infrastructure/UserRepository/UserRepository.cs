using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.UserRepository
{
    public class UserRepository : BaseRepository<SystemUser, SystemUserId>, IUserRepository
    {
    
    public UserRepository(DBContext context):base(context.Users)
        {   
        }


    }
}