using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.ConfirmationTokenRepository
{
    public interface IConfirmationTokenRepository : IRepository<ConfirmationToken,ConfirmationTokenId>
    {
    }
}