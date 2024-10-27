using Sempi5.Domain.ConfirmationLinkAggregate;
using Sempi5.Domain.ConfirmationTokenAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.ConfirmationLinkAggregate;


public interface IConfirmationLinkRepository : IRepository<ConfirmationLink,ConfirmationTokenId>
{
    public Task<ConfirmationLink> GetByIdAndNotUsed(Guid id);
        
    public Task<ConfirmationLink> GetByIdAndNotUsed(string id);
    public Task saveAsyc();
    public Task<ConfirmationLink> GetByEmail(string email);

}