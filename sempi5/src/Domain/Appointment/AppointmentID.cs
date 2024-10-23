namespace Sempi5.Domain.Shared;

public class AppointmentID : EntityId
{
    public AppointmentID(int value) : base(value)
    {
    }

    protected override object createFromString(string text)
    {
        return text;
    }

    public override string AsString()
    {
        return (string)ObjValue;    
    }
}