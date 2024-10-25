using Sempi5.Domain.OperationRequestAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.OperationRequestRepository;

public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestID>
{
    
}