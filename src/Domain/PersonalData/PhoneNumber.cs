namespace Sempi5.Domain.PersonalData
{
    public class PhoneNumber
    {
        private readonly int _phoneNumber;
        
        public PhoneNumber(int phoneNumber)
        {
            ValidatePhoneNumber(phoneNumber);
            _phoneNumber = phoneNumber;
        }

        private void ValidatePhoneNumber(int phoneNumber)
        {
            if (phoneNumber.ToString().Length != 9)
            {
                throw new ArgumentException("Phone number must be 9 digits long");
            }
        }

        public int phoneNumber()
        {
            return _phoneNumber;
        }
    }
}