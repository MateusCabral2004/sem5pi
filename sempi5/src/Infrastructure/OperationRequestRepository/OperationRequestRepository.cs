using Sempi5.Domain.OperationRequest;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.OperationRequest;

public class OperationRequestRepository : BaseRepository<Domain.OperationRequest.OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    private readonly DBContext context;
    
    public OperationRequestRepository(DBContext dbContext) : base(dbContext.OperationRequests)
    {
        this.context = dbContext;
    }
}