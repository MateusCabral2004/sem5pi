using Sempi5.Domain.Shared;

namespace Sempi5.Domain.User
{
    public class SystemUser : Entity<SystemUserId>
    {
        public new SystemUserId Id { get; set; }
        public Email Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsVerified { get; set; }
        
        
        public SystemUser(Email email, string role)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(role);
            ArgumentException.ThrowIfNullOrWhiteSpace(role);
            Email = email;
            Username = email.ToString();
            Role = role;
            IsVerified = false;
        }
        
        public SystemUser(Email email, string role, bool isVerified)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(role);
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