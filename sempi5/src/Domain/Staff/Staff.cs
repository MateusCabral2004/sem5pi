using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Staff
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {

        public SystemUser? User { get; set; }
        public StaffId Id { get; set; }
        public Person Person { get; set; }
        public LicenseNumber LicenseNumber { get; set; }
        public Specialization.Specialization Specialization { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        
        private Staff() { }
        
        public Staff(SystemUser user, LicenseNumber licenseNumber, Name firstName, Name lastName, Specialization.Specialization specialization, ContactInfo contactInfo, List<string> availabilitySlots)
        {
            User = user;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = availabilitySlots;
            Person = new Person(firstName, lastName, contactInfo);
        }
        
        public Staff(LicenseNumber licenseNumber, Name firstName, Name lastName, Specialization.Specialization specialization, ContactInfo contactInfo, List<string> availabilitySlots)
        {
            User = null;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = availabilitySlots;
            Person = new Person(firstName, lastName, contactInfo);
        }        
        
        public void AddUser(SystemUser user)
        {
            User = user;
        }
        
     
        
    }
}