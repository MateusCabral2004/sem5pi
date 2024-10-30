using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SurgeryRoomAggregate;

public class RoomNumber : EntityId, IValueObject
{
        
    public RoomNumber(int value) : base(value)
    {}
        
    public override string AsString()
    {
        return ObjValue.ToString();
    }
        
    public int AsInt()
    {
        return int.Parse(ObjValue.ToString());
    }

    protected override object createFromString(string text)
    {
        return int.Parse(text);
    }
    
}