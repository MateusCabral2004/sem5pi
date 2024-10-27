using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.ConfirmationLinkAggregate;
using Sempi5.Domain.ConfirmationTokenAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.ConfirmationLinkAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.LinkConfirmationRepository;

public class ConfirmationLinkRepository : BaseRepository<ConfirmationLink, ConfirmationTokenId>,
    IConfirmationLinkRepository
{
    private readonly DBContext context;

    public ConfirmationLinkRepository(DBContext context) : base(context.ConfirmationLink)
    {
        this.context = context;
    }

    public async Task<ConfirmationLink> GetByIdAndNotUsed(Guid id)
    {
        var token = await context.ConfirmationLink
            .Where(c => c.Id == id && c.IsUsed == false && c.ExpiryDate > DateTime.Now)
            .FirstOrDefaultAsync();

        return token;
    }

    public async Task<ConfirmationLink> addAsync(ConfirmationLink obj)
{
    var result = await context.ConfirmationLink.AddAsync(obj);
    await context.SaveChangesAsync();
    return result.Entity;
}

    public async Task<ConfirmationLink> GetByIdAndNotUsed(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return await GetByIdAndNotUsed(guid);
        }

        throw new ArgumentException("Invalid Guid format", nameof(id));
    }

   

    public async Task saveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public Task<ConfirmationLink> GetByEmail(string email)
    {
        Email emailObj = new Email(email);

        return context.ConfirmationLink
            .Where(c => c.email.Equals(emailObj))
            .FirstOrDefaultAsync();
    }
}