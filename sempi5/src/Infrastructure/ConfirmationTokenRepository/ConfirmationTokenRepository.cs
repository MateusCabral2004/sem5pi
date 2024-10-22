using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.ConfirmationTokenRepository
{
    public class ConfirmationTokenRepository : BaseRepository<ConfirmationToken,ConfirmationTokenId>, IConfirmationTokenRepository
    {

        private readonly DBContext context;
        
        public ConfirmationTokenRepository(DBContext context) : base(context.ConfirmationTokens)
        { this.context = context; }
        
        
    }
}