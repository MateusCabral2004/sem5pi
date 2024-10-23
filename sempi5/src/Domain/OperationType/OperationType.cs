using NuGet.Packaging.Signing;

namespace Sempi5.Domain.OperationType;

public class OperationType
{
    public long Id { get; set; }
    public OperationName Name { get; set; }
    public List<RequiredStaff> RequiredStaff { get; set; }
    public TimeSpan Duration { get; set; }
    
}