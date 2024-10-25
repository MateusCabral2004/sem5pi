using Sempi5.Domain.ConfirmationLink;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.LinkConfirmationRepository;

public interface IConfirmationLinkRepository : IRepository<ConfirmationLink,ConfirmationTokenId>
{
    public Task<ConfirmationLink> GetByIdAndNotUsed(Guid id);
        
    public Task<ConfirmationLink> GetByIdAndNotUsed(string id);
    public Task saveAsyc();
    public Task<ConfirmationLink> GetByEmail(string email);

}