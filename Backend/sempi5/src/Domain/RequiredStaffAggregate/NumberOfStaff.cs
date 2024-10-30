using Sempi5.Domain.Shared;

namespace Sempi5.Domain.RequiredStaffAggregate;

public class NumberOfStaff : IValueObject
{
    public virtual int value { get; private set; }

    private NumberOfStaff() {}
    
    public NumberOfStaff(int value)
    {
        if(value <= 0)
        {
            throw new ArgumentException("Number of staff cannot be negative");
        }
        this.value = value;
    }

    public virtual int getValue()
    {
        return value;
    }
    
    public virtual bool Equals (NumberOfStaff other)
    {
        if(other == null)
        {
            return false;
        }
        return this.value == other.value;
    }
}