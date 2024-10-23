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