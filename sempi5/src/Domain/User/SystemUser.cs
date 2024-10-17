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

        
        public SystemUser(string email, string role)
        {
            Email = new Email(email);
            Username = email;
            Role = role;
            IsVerified = false;
        }
        
        public SystemUser(Email email, string role)
        {
            Email = email;
            Username = email.ToString();
            Role = role;
            IsVerified = false;
        }
        
        public SystemUser(Email email, string role, bool isVerified)
        {
            Email = email;
            Username = email.ToString();
            
            Role = role;
            IsVerified = isVerified;
        }
    }
    
    
    //TODO: Rever Roles to Enum
}