using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.OperationRequestRepository;

public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    private readonly DBContext context;
    
    public OperationRequestRepository(DBContext dbContext) : base(dbContext.OperationRequests)
    {
        this.context = dbContext;
    }
}