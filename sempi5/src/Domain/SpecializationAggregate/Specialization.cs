using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SpecializationAggregate;

public class Specialization : Entity<SpecializationID>, IAggregateRoot
{
    public SpecializationID Id { get; set; }
    
    public SpecializationName specializationName { get; set; }

    private Specialization() { }
    
    public Specialization(SpecializationName specializationName)
    {
        this.specializationName = specializationName;
    }

    private sealed class SpecializationNameEqualityComparer : IEqualityComparer<Specialization>
    {
        public bool Equals(Specialization? x, Specialization? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.specializationName.Equals(y.specializationName);
        }

        public int GetHashCode(Specialization obj)
        {
            return obj.specializationName.GetHashCode();
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is Specialization other)
        {
            // Compare the specializationName properties
            return specializationName.ToString().Equals(other.specializationName.ToString());
        }
        return false;
    }
    
}