using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Specialization;

public class Specialization : Entity<SpecializationID>, IAggregateRoot
{
    public SpecializationID Id { get; set; }
    
    public SpecializationName specializationName { get; set; }

    private Specialization() { }
    
    public Specialization(SpecializationName specializationName)
    {
        this.specializationName = specializationName;
    }
}