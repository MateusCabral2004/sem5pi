namespace Sempi5.Domain.Shared;

public class AppointmentID : EntityId
{
    public AppointmentID(long value) : base(value)
    {
    }
    

    protected override object createFromString(string text)
    {
        return text;
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