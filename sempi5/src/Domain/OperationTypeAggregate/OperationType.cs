using NuGet.Packaging.Signing;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;

namespace Sempi5.Domain.OperationTypeAggregate;

public class OperationType : Entity<OperationTypeID>, IAggregateRoot
{
    public OperationTypeID Id { get; set; }
    
    public OperationName Name { get; set; }
    
    public List<RequiredStaff> RequiredStaff { get; set; }
    
    public TimeSpan SetupDuration { get; set; }
    public TimeSpan SurgeryDuration { get; set; }
    public TimeSpan CleaningDuration { get; set; }
    
    public bool stillPerformed { get; set; }
    
    private OperationType() { }
    
    public OperationType(OperationName name, List<RequiredStaff> requiredStaff, TimeSpan setupDuration, TimeSpan surgeryDuration, TimeSpan cleaningDuration)
    {
        Name = name;
        RequiredStaff = requiredStaff;
        SetupDuration = setupDuration;
        SurgeryDuration = surgeryDuration;
        CleaningDuration = cleaningDuration;
        stillPerformed = true;
    }
    
    public OperationType(OperationName name, TimeSpan setupDuration, TimeSpan surgeryDuration, TimeSpan cleaningDuration)
    {
        Name = name;
        RequiredStaff = [];
        SetupDuration = setupDuration;
        SurgeryDuration = surgeryDuration;
        CleaningDuration = cleaningDuration;
        this.stillPerformed = true;
    }
    
    public void AddRequiredStaff(RequiredStaff requiredStaff)
    {
        RequiredStaff.Add(requiredStaff);
    }
    
    public void RemoveRequiredStaff(RequiredStaff requiredStaff)
    {
        RequiredStaff.Remove(requiredStaff);
    }
    
    public void MarkAsNoLongerPerformed()
    {
        this.stillPerformed = false;   
    }
    
}