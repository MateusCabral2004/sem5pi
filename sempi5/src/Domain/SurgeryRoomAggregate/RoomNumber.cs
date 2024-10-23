using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SurgeryRoomAggregate;

public class RoomNumber : EntityId
{
    public RoomNumber() : base(null) // or you can choose to set it to null if appropriate
    {}
        
    public RoomNumber(int value) : base(value)
    {}
        
    public override string AsString()
    {
        return (string)base.ObjValue.ToString();
    }
        
    public int AsLong()
    {
        return (int)base.ObjValue;
    }

    protected override object createFromString(string text)
    {
        return int.Parse(text);
    }
    
}