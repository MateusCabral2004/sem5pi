using Sempi5.Domain.OperationType;
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
    
    
}
