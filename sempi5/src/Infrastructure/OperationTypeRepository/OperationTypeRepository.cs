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
    
    
}
