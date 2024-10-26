using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.OperationTypeRepository;

public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeID>, IOperationTypeRepository
{
    private readonly DBContext context;
    
    public OperationTypeRepository(DBContext dbContext) : base(dbContext.OperationTypes)
    {
        this.context = dbContext;
    }
    
    public async Task<OperationType> GetOperationTypeByName(OperationName name)
    {
        return await context.OperationTypes
            .Include(x => x.RequiredStaff)
            .ThenInclude(r => r.Specialization)
            .FirstOrDefaultAsync(x => x.Name.Equals(name));
    }

    public async Task<List<OperationType>> GetOperationTypeListByName(OperationName name)
    {
        if (name == null)
        {
            return null;
        }

        return await context.OperationTypes
            .Include(o => o.Name)
            .Include(o => o.RequiredStaff)
            .Include(o => o.SurgeryDuration)
            .Include(o => o.SetupDuration)
            .Include(o => o.CleaningDuration)
            .Where(o => o.Name.Equals(name))
            .ToListAsync();
    }

    public async Task<List<OperationType>> GetOperationTypeListBySpecialization(SpecializationName specializationName)
    {
        if (specializationName == null)
        {
            return null;
        }

        return await context.OperationTypes
            .Include(o=>o.Name)
            .Include(o=>o.RequiredStaff)
            .Include(o=>o.SurgeryDuration)
            .Include(o=>o.SetupDuration)
            .Include(o=>o.CleaningDuration)
            .Where(o=>o.RequiredStaff.Exists(r=>r.Specialization.specializationName.Equals(specializationName)))
            .ToListAsync();
        
    }

    public async Task<List<OperationType>> GetOperationTypeListByStatus(OperationType status)
    {
        if (status == null)
        {
            return null;
        }

        return await context.OperationTypes
            .Include(o => o.Name)
            .Include(o => o.RequiredStaff)
            .Include(o => o.SurgeryDuration)
            .Include(o => o.SetupDuration)
            .Include(o => o.CleaningDuration)
            .Where(o => o.stillPerformed.Equals(status))
            .ToListAsync();
    }

    
    
}
