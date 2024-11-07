namespace Sempi5.Domain.StaffAggregate.StaffExceptions;

public class NoStaffProfilesException : Exception
{
    public int ErrorCode { get; }
    
    public NoStaffProfilesException(string message) : base(message)
    {
        ErrorCode = 298;
    }
    
}