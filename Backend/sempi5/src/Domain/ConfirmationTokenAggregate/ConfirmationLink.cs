using Sempi5.Domain.ConfirmationTokenAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.ConfirmationLinkAggregate;

public class ConfirmationLink : Entity<ConfirmationTokenId>, IAggregateRoot
{
    public Guid Id { get; set; } //acts as the token
    public Email email { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public ConfirmationLink(Email email)
    {
        ArgumentNullException.ThrowIfNull(email);
        this.email = email;
        ExpiryDate = DateTime.Now.AddDays(1);
        IsUsed = false;
    }
    
    public void ResetExpiryDate()
    {
        ExpiryDate = DateTime.Now.AddDays(1);
    }
    
    public void Use()
    {
        IsUsed = true;
    }
    
    public bool IsExpired()
    {
        return ExpiryDate < DateTime.Now;
    }
}