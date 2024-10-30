using System.Security.Permissions;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;

namespace Sempi5.Domain.RequiredStaffAggregate;

public class RequiredStaff : Entity<RequiredStaffID>, IAggregateRoot
{
    public RequiredStaffID Id { get; set; }
    
    public NumberOfStaff NumberOfStaff { get; set; }
    
    public Specialization Specialization { get; set; }
    
    private RequiredStaff() {}
    
    public RequiredStaff(NumberOfStaff numberOfStaff, Specialization specialization)
    {
        ArgumentNullException.ThrowIfNull(numberOfStaff);
        ArgumentNullException.ThrowIfNull(specialization);
        
        this.NumberOfStaff = numberOfStaff;
        this.Specialization = specialization;
    }
    
    public bool Equals(RequiredStaff other)
    {
        if(other == null)
        {
            return false;
        }
        return this.NumberOfStaff.Equals(other.NumberOfStaff) && this.Specialization.Equals(other.Specialization);
    }
}