using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Domain.Staff;

namespace Sempi5.Domain.Staff
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {

        public SystemUser User { get; set; }
        public StaffId Id { get; set; }
        public LicenseNumber LicenseNumber { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Name FullName { get; set; }  //TODO: Factory / Builder
        public string Specialization { get; set; } //TODO: ENUM
        public ContactInfo ContactInfo { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        
        private Staff() { }
        
        public Staff(SystemUser user, LicenseNumber licenseNumber, Name firstName, Name lastName, string specialization, ContactInfo contactInfo, List<string> availabilitySlots)
        {
            User = user;
            LicenseNumber = licenseNumber;
            FirstName = firstName;
            LastName = lastName;
            FullName = new Name(firstName + " " + lastName);
            Specialization = specialization;
            ContactInfo = contactInfo;
            AvailabilitySlots = availabilitySlots;
        }
     
        
    }
}