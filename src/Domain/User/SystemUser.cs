using Sempi5.Domain.Shared;

namespace Sempi5.Domain.User
{
    public class SystemUser : Entity<SystemUserId>
    {
        public SystemUserId Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public SystemUser(string email, string role)
        {
            Email = email;
            Username = email;
            Role = role;
        }
    }
}