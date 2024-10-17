using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Domain.PersonalData
{

    public class Person : Entity<PersonId>, IAggregateRoot
    {
        public long Id { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Name FullName { get; set; }
        public ContactInfo ContactInfo { get; set; }

        private Person() { }

        public Person(Name firstName, Name lastName, ContactInfo contactInfo)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = new Name($"{firstName.ToString()} {lastName.ToString()}");
            ContactInfo = contactInfo;
        }
    }
}