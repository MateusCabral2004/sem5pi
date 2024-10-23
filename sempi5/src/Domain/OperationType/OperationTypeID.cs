using Sempi5.Domain.Shared;

namespace Sempi5.Domain.OperationType;

public class OperationTypeID : EntityId
{
    
    public OperationTypeID() : base(null)
    {
    }
    
    public OperationTypeID(long value) : base(value)
    {
    }
    
    public long AsLong()
    {
        return long.Parse(ObjValue.ToString());
    }
    
    override
    public string AsString()
    {
        return ObjValue.ToString();
    }

    protected override object createFromString(string text)
    {
        return text;
    }
    
}
