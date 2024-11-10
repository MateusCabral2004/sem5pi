namespace Sempi5.Domain.StaffAggregate.StaffExceptions;

public class LicenseNumberAlreadyInUseException : Exception
{
    public LicenseNumberAlreadyInUseException(string message) : base(message) { }
    
}