using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SpecializationAggregate;

public class SpecializationID : EntityId
{
    public SpecializationID() : base(null)
    {
    }
    
    public SpecializationID(long value) : base(value)
    {
    }
    
    protected override object createFromString(string text)
    {
        return long.Parse(text);
    }

    public long AsLong()
    {
        return (long)ObjValue;
    }
    
    public override string AsString()
    {
        return ObjValue.ToString();    
    }
        
    
}