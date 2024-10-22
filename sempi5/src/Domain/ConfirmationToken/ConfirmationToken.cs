using Sempi5.Domain.Shared;

namespace Sempi5.Domain.ConfirmationToken;

public class ConfirmationToken : Entity<ConfirmationTokenId>, IAggregateRoot
{
    public Guid Id { get; set; } //acts as the token
    public Email email { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    
    public ConfirmationToken(Email email)
    {
        ArgumentNullException.ThrowIfNull(email);
        this.email = email;
        ExpiryDate = DateTime.Now.AddDays(1);
        IsUsed = false;
    }
    
    public void Use()
    {
        IsUsed = true;
    }
}