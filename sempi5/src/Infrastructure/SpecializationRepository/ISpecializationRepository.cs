using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Specialization;

namespace Sempi5.Infrastructure.SpecializationRepository
{
    public interface ISpecializationRepository : IRepository<Specialization, SpecializationID>
    {
    }
}