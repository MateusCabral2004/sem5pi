using Sempi5.Domain.SurgeryRoomAggregate;

namespace Sempi5.Domain.Shared;

public class Appointment : Entity<AppointmentID>, IAggregateRoot
{
    public AppointmentID Id { get; set; }
    
    public OperationRequest.OperationRequest OperationRequest { get; set; }
    
    public SurgeryRoom SurgeryRoom { get; set; }
    
    public DateTime Date { get; set; }
    
    public StatusEnum Status { get; set; }
    
    
    public Appointment() {}
    
    public Appointment(OperationRequest.OperationRequest operationRequest,SurgeryRoom surgeryRoom, DateTime date, StatusEnum status)
    {
        OperationRequest = operationRequest;
        SurgeryRoom = surgeryRoom;
        Date = date;
        Status = status;
    }
    
}