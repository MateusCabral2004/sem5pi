using Sempi5.Domain.AccoutToDelete;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.AccoutToDeleteRepository;

public interface IAccountToDeleteRepository: IRepository<AccoutToDelete, SystemUserId>
{
    Task<int> saveUserToDelete(long userId);
}