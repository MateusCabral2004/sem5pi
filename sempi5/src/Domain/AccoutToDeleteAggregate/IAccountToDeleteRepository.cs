using Sempi5.Domain.AccoutToDeleteAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.AccoutToDeleteAggregate;

public interface IAccountToDeleteRepository: IRepository<AccoutToDelete, SystemUserId>
{
    Task<int> saveUserToDelete(long userId);
}