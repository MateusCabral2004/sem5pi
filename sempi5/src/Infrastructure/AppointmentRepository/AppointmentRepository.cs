using Microsoft.EntityFrameworkCore;
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
}