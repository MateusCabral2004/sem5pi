using Sempi5.Domain.OperationType;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.OperationTypeRepository;

public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeID>
{
    
}