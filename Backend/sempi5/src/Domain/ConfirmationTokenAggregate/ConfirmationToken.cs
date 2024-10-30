using Sempi5.Domain.Shared;

namespace Sempi5.Domain.ConfirmationTokenAggregate;

public class ConfirmationToken : Entity<ConfirmationTokenId>, IAggregateRoot
{
    public Guid Id { get; set; } //acts as the token
    public Email email { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public string IdToAssociate { get; set; }
    
    private ConfirmationToken(){}
    
    public ConfirmationToken(Email email,string id)
    {
        ArgumentNullException.ThrowIfNull(email);
        this.email = email;
        ExpiryDate = DateTime.Now.AddDays(1);
        IsUsed = false;
        IdToAssociate = id;
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