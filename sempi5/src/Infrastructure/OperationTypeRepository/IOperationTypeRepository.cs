using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.OperationTypeRepository;

public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeID>
{
    public Task<OperationType> GetOperationTypeByName(OperationName name);
}