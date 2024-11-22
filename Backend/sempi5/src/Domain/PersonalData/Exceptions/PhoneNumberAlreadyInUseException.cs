namespace Sempi5.Domain.PersonalData.Exceptions;

public class PhoneNumberAlreadyInUseException : Exception
{
    public PhoneNumberAlreadyInUseException(string message) : base(message)
    {
    }
}