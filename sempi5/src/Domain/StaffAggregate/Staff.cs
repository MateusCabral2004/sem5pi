using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Domain.SpecializationAggregate;

namespace Sempi5.Domain.StaffAggregate
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {

        public SystemUser? User { get; set; }
        public StaffId Id { get; set; }
        public Person Person { get; set; }
        public LicenseNumber LicenseNumber { get; set; }
        public Specialization Specialization { get; set; }
        public List<string> AvailabilitySlots { get; set; }
        public StaffStatusEnum Status { get; set; }
        
        private Staff() { }
        
        public Staff(LicenseNumber licenseNumber, Person person, Specialization specialization)
        {
            
            ArgumentNullException.ThrowIfNull(licenseNumber);
            ArgumentNullException.ThrowIfNull(person);
            ArgumentNullException.ThrowIfNull(specialization);
            
            User = null;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = new List<string>();
            Person = person;
            Status = StaffStatusEnum.INACTIVE;
        }
        //Apagar
        public Staff(SystemUser user, LicenseNumber licenseNumber, Name firstName, Name lastName, Specialization specialization, ContactInfo contactInfo, List<string> availabilitySlots)
        {
            User = user;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = availabilitySlots;
            Person = new Person(firstName, lastName, contactInfo);
            Status = StaffStatusEnum.ACTIVE;
        }
        //Apagar
        public Staff(SystemUser user, LicenseNumber licenseNumber, Name firstName, Name lastName, Specialization specialization, ContactInfo contactInfo, List<string> availabilitySlots, StaffStatusEnum status)
        {
            User = user;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = availabilitySlots;
            Person = new Person(firstName, lastName, contactInfo);
            Status = status;
        }
        
        
        //Apagar
        public Staff(LicenseNumber licenseNumber, Name firstName, Name lastName, Specialization specialization, ContactInfo contactInfo, List<string> availabilitySlots)
        {
            User = null;
            LicenseNumber = licenseNumber;
            Specialization = specialization;
            AvailabilitySlots = availabilitySlots;
            Person = new Person(firstName, lastName, contactInfo);
            Status = StaffStatusEnum.ACTIVE;
        }        
        
        public void AddUser(SystemUser user)
        {
            User = user;
        }
        
        public void markAsActive()
        {
            Status = StaffStatusEnum.ACTIVE;
        }
        
    }
}