using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Domain.Staff;

namespace Sempi5.Domain.Staff
{
    public class Staff : IAggregateRoot
    {

        //Class can and should be modified so that fields are more representative 
        //of what they are instead of just strings
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Specialization { get; set; }
        public string ContactInfo { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        public string? Password { get; set; }
        public SystemUser User { get; set; }

    }
}