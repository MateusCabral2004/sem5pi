using Sempi5.Domain;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.PersonRepository
{
    public interface IPersonRepository : IRepository<Person,PersonId>
    {
    }
}
