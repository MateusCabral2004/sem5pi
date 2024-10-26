using Microsoft.EntityFrameworkCore;
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

    public async Task<OperationRequest> GetOperationRequestById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }
        
        var operationRequest=context.OperationRequests
            .Include(r=>r.Doctor)
            .AsEnumerable()
            .FirstOrDefault(o=>o.Id.AsString().Equals(id));

        return operationRequest;
    }
}