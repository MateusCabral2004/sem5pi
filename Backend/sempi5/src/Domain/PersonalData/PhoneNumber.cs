using Sempi5.Domain.PersonalData.Exceptions;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.PersonalData
{
    public class PhoneNumber : IValueObject
    {
        private int  _phoneNumber;
        
        public PhoneNumber(int phoneNumber)
        {
            ValidatePhoneNumber(phoneNumber);
            _phoneNumber = phoneNumber;
        }

        public PhoneNumber()
        {
            
        }

        private void ValidatePhoneNumber(int phoneNumber)
        {

            if (phoneNumber < 0)
            {
                throw new InvalidPhoneNumberFormat("Phone number cannot be negative.");
            }
            
            if (phoneNumber.ToString().Length != 9 || phoneNumber <= 0)
            {
                throw new InvalidPhoneNumberFormat("Phone number must be 9 digits long.");
            }
        }

        public virtual int phoneNumber()
        {
            return _phoneNumber;
        }
        
        public bool Equals(PhoneNumber phoneNumber)
        {
            if(phoneNumber == null)
            {
                return false;
            }
            
            return phoneNumber._phoneNumber == _phoneNumber;
        }
    }
}