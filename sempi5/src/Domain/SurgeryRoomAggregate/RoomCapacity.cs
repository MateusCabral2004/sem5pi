namespace Sempi5.Domain.SurgeryRoomAggregate;

public class RoomCapacity
{
    public int Capacity { get; private set; }

    public RoomCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Room capacity must be greater than 0");
        }

        Capacity = capacity;
    }
    
    public int AsInt()
    {
        return Capacity;
    }
}