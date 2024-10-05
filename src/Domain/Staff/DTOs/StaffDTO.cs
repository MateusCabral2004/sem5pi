namespace Sempi5.Domain.Staff.DTOs
{
    public class StaffDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        public string ContactInfo { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
