using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SurgeryRoomAggregate;

public class SurgeryRoom : Entity<RoomNumber>, IAggregateRoot
{
    public RoomNumber Id { get; set; }
    public RoomTypeEnum Type { get;  set; }
    public RoomCapacity Capacity { get;  set; }
    public List<string> Equipment { get;  set; }
    public RoomStatusEnum Status { get;  set; }
    public List<string> MaintenanceSlots { get;  set; }
    
    private SurgeryRoom() { }
    
    public SurgeryRoom(RoomTypeEnum type, RoomCapacity capacity, List<string> equipment, RoomStatusEnum status, List<string> maintenanceSlots)
    {
        Type = type;
        Capacity = capacity;
        Equipment = equipment;
        Status = status;
        MaintenanceSlots = maintenanceSlots;
    }
    
}