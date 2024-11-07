namespace Sempi5.Domain.StaffAggregate.StaffExceptions;

public class StaffProfileNotFoundException : Exception
{
    public int ErrorCode { get; }
    
    public StaffProfileNotFoundException(string message) : base(message)
    {
        ErrorCode = 299;
    }
    
}