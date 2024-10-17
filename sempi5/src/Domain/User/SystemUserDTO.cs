namespace Sempi5.Domain.User
{
    public class SystemUserDTO
    {
        public long? Id { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }
    }
}