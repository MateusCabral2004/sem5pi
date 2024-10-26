using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;

namespace Sempi5.Infrastructure.SpecializationRepository
{
    public interface ISpecializationRepository : IRepository<Specialization, SpecializationID>
    {
        
        public Task<Specialization?> GetBySpecializationName(Specialization specialization);
        
    }
}