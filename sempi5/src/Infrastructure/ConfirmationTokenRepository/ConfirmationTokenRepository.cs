using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.ConfirmationTokenRepository
{
    public class ConfirmationTokenRepository : BaseRepository<ConfirmationToken, ConfirmationTokenId>,
        IConfirmationTokenRepository
    {
        private readonly DBContext context;

        public ConfirmationTokenRepository(DBContext context) : base(context.ConfirmationTokens)
        {
            this.context = context;
        }

        public async Task<ConfirmationToken> GetByIdAndNotUsed(Guid id)
        {
            var token = await context.ConfirmationTokens
                .Where(c => c.Id == id && c.IsUsed == false && c.ExpiryDate > DateTime.Now)
                .FirstOrDefaultAsync();

            return token;
        }

        public async Task<ConfirmationToken> GetByIdAndNotUsed(string id)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                return await GetByIdAndNotUsed(guid);
            }
            throw new ArgumentException("Invalid Guid format", nameof(id));
        }
    }
}