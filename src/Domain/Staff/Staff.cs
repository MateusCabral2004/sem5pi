using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Domain.Staff;

namespace Sempi5.Domain.Staff
{
    public class Staff : Entity<LicenseNumber>, IAggregateRoot
    {

        public LicenseNumber Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string ContactInfo { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        public string? Password { get; set; }
        public SystemUser User { get; set; }

    }
}