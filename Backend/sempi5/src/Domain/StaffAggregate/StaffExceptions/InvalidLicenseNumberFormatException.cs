namespace Sempi5.Domain.StaffAggregate.StaffExceptions;

public class InvalidLicenseNumberFormatException : Exception
{
    public InvalidLicenseNumberFormatException(string message) : base(message)
    {
    }
    
}