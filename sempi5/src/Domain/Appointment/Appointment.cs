namespace Sempi5.Domain.Shared;

public class Appointment : Entity<AppointmentID>, IAggregateRoot
{
    public AppointmentID Id { get; set; }
    
    
}