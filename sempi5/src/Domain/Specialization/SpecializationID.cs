using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Specialization;

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
        return int.Parse(text);
    }
    
    public override string AsString()
    {
        return (string)ObjValue;    
    }
        
    
}