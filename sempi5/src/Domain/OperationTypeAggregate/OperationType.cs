using NuGet.Packaging.Signing;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Domain.OperationTypeAggregate;

public class OperationType : Entity<OperationTypeID>, IAggregateRoot
{
    public OperationTypeID Id { get; set; }
    
    public OperationName Name { get; set; }
    
    public List<RequiredStaff> RequiredStaff { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public bool stillPerformed { get; set; }
    
    private OperationType() { }
    
    public OperationType(OperationName name, List<RequiredStaff> requiredStaff, TimeSpan duration)
    {
        Name = name;
        RequiredStaff = requiredStaff;
        Duration = duration;
        stillPerformed = true;
    }
}