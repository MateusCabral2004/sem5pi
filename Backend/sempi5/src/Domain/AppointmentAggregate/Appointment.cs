using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SurgeryRoomAggregate;

namespace Sempi5.Domain.AppointmentAggregate;

public class Appointment : Entity<AppointmentID>, IAggregateRoot
{
    public AppointmentID Id { get; set; }
    
    public OperationRequest? OperationRequest { get; set; }
    
    public SurgeryRoom SurgeryRoom { get; set; }
    
    public DateTime Date { get; set; }
    
    public StatusEnum Status { get; set; }
    
    
    public Appointment() {}
    
    public Appointment(OperationRequest operationRequest,SurgeryRoom surgeryRoom, DateTime date, StatusEnum status)
    {
        ArgumentNullException.ThrowIfNull(operationRequest);
        ArgumentNullException.ThrowIfNull(surgeryRoom);
        ArgumentNullException.ThrowIfNull(date);
        ArgumentNullException.ThrowIfNull(status);

        OperationRequest = operationRequest;
        SurgeryRoom = surgeryRoom;
        Date = date;
        Status = status;
    }
    
}