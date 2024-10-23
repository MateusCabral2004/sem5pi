using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.OperationRequest;

public interface IOperationRequestRepository : IRepository<Domain.OperationRequest.OperationRequest, Domain.OperationRequest.OperationRequestID>
{
    
}