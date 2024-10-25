using System.Security.Permissions;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;

namespace Sempi5.Domain.OperationType;

public class RequiredStaff : Entity<RequiredStaffID>, IAggregateRoot
{
    public RequiredStaffID Id { get; set; }
    
    public NumberOfStaff NumberOfStaff { get; set; }
    
    public Specialization Specialization { get; set; }
    
    private RequiredStaff() {}
    
    public RequiredStaff(NumberOfStaff numberOfStaff, Specialization specialization)
    {
        this.NumberOfStaff = numberOfStaff;
        this.Specialization = specialization;
    }
    
}