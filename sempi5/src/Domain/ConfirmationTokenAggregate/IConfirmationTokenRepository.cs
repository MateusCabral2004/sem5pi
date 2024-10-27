using Sempi5.Domain.ConfirmationTokenAggregate;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.ConfirmationTokenAggregate
{
    public interface IConfirmationTokenRepository : IRepository<ConfirmationToken,ConfirmationTokenId>
    {
        public Task<ConfirmationToken> GetByIdAndNotUsed(Guid id);
        
        public Task<ConfirmationToken> GetByIdAndNotUsed(string id);
        public Task saveAsyc(ConfirmationToken confirmationToken);
        public Task<ConfirmationToken> GetByEmail(string email);

    }
}