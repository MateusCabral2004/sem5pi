using Sempi5.Domain.Shared;

namespace Sempi5.Domain.OperationRequestAggregate;

public class OperationRequestID : EntityId, IValueObject
{
    
    public OperationRequestID() : base(null)
    {
    }
    
    public OperationRequestID(long value) : base(value)
    {
    }

    public long AsLong()
    {
        return long.Parse(ObjValue.ToString());
    }
    
    protected override object createFromString(string text)
    {
        return text;
    }

    public override string AsString()
    {
        return ObjValue.ToString();
    }
}