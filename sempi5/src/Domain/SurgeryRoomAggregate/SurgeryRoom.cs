using Sempi5.Domain.Shared;

namespace Sempi5.Domain.SurgeryRoomAggregate;

public class SurgeryRoom : Entity<RoomNumber>, IAggregateRoot
{
    public RoomNumber Id { get; private set; }
    public RoomTypeEnum Type { get; private set; }
    public RoomCapacity Capacity { get; private set; }
    public List<string> Equipment { get; private set; }
    public RoomTypeEnum Status { get; private set; }
    public List<string> MaintenanceSlots { get; private set; }
}