namespace Sempi5.Domain.PersonalData.Exceptions;

public class InvalidPhoneNumberFormat : Exception
{
    public InvalidPhoneNumberFormat(string message) : base(message)
    {
    }
    
}