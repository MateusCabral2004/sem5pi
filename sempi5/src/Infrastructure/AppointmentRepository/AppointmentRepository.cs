using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.AppointmentAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.AppointmentRepository;

public class AppointmentRepository : BaseRepository<Appointment, AppointmentID>, IAppointmentRepository
{
    private readonly DBContext context;

    public AppointmentRepository(DBContext dbContext) : base(dbContext.Appointments)
    {
        this.context = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Appointment?> getAppointmentByOperationRequestID(long id)
    {
        return context.Appointments.AsEnumerable().FirstOrDefault(a => a.OperationRequest.Id.AsLong() == id);
    }


    public async Task updataAppointment(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        await context.SaveChangesAsync();
    }
}