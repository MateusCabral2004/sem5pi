using Sempi5.Domain.Shared;

namespace Sempi5.Domain.OperationType;

public class RequiredStaffID : EntityId
{
 
    public RequiredStaffID() : base(null)
    {
    }
    
    public RequiredStaffID(long value) : base(value)
    {
    }
    
    protected override object createFromString(string text)
    {
        return int.Parse(text);
    }
    
    public long AsLong()
    {
        return long.Parse(ObjValue.ToString());
    }
    
    public override string AsString()
    {
        return ObjValue.ToString();    
    }
    
}