using Sempi5.Domain.AccoutToDeleteAggregate;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.AccoutToDeleteAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.AccoutToDeleteRepository;

public class AccountToDeleteRepository : BaseRepository<AccoutToDelete, SystemUserId>, IAccountToDeleteRepository
{
    private readonly DBContext context;

    public AccountToDeleteRepository(DBContext dbContext) : base(dbContext.AccoutToDelete)
    {
        this.context = dbContext;
    }

    public async Task<int> saveUserToDelete(long userId)
    {
        var accountToDelete = new AccoutToDelete(new SystemUserId(userId), DateTime.Now.AddDays(30));
        Task.Run(() => context.AccoutToDelete.Add(accountToDelete));
        return await context.SaveChangesAsync();
    }

    public async Task<List<SystemUserId>> checkUserToDelete()
    {
        return await Task.Run(() =>
            context.AccoutToDelete.Where(x => x.DateToDelete < DateTime.Now).Select(x => x.Id).ToList());
    }
    
    public async Task removeUserbyId(SystemUserId userId)
    {
        var accountToDelete = await context.AccoutToDelete.FindAsync(userId);

        if (accountToDelete != null)
        {
            context.AccoutToDelete.Remove(accountToDelete);
        }
    }

}