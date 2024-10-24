using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.ConfirmationTokenRepository
{
    public interface IConfirmationTokenRepository : IRepository<ConfirmationToken,ConfirmationTokenId>
    {
        public Task<ConfirmationToken> GetByIdAndNotUsed(Guid id);
        
        public Task<ConfirmationToken> GetByIdAndNotUsed(string id);
        public Task saveAsyc(ConfirmationToken confirmationToken);
        public Task<ConfirmationToken> GetByEmail(string email);

    }
}