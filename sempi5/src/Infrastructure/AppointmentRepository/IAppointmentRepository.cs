using Sempi5.Domain.AppointmentAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.AppointmentRepository;

public interface IAppointmentRepository : IRepository<Appointment, AppointmentID>
{
    
}