using Sempi5.Domain.Shared;

namespace Sempi5.Domain.PersonalData
{
    public class ContactInfo
    {
        public Email _email { get; set; }
        public PhoneNumber _phoneNumber { get; set; }
        
        public ContactInfo(Email email, PhoneNumber phoneNumber)
        {
            _email = email;
            _phoneNumber = phoneNumber;
        }

        public ContactInfo(string email, int phoneNumber)
        {
            _email = new Email(email);
            _phoneNumber = new PhoneNumber(phoneNumber);
        }
        
 

        public Email email()
        {
            return _email;
        }

        public PhoneNumber phoneNumber()
        {
            return _phoneNumber;
        }

    }
}