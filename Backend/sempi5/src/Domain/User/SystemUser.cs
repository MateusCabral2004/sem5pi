using Sempi5.Domain.Shared;

namespace Sempi5.Domain.User
{
    public class SystemUser : Entity<SystemUserId>, IAggregateRoot
    {
        public virtual SystemUserId Id { get; set; }
        public virtual Email Email { get; set; }
        public virtual string Username { get; set; }
        public virtual string Role { get; set; }
        public virtual bool IsVerified { get; set; }
        
        
        public SystemUser(Email email, string role)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(role);
            Email = email;
            Username = email.ToString();
            Role = role;
            IsVerified = false;
        }
        
        public SystemUser(Email email, string role, bool isVerified)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(role);
            Email = email;
            Username = email.ToString();
            Role = role;
            IsVerified = isVerified;
        }
        
        public void Verify()
        {
            IsVerified = true;
        }
    }
    
    
    //TODO: Rever Roles to Enum
}