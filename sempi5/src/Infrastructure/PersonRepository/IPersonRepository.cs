using Sempi5.Domain;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.PersonRepository
{
    public interface IPersonRepository : IRepository<Person,PersonId>
    {
        
        public Task<Person?> GetPersonByPhoneNumber(PhoneNumber phoneNumber);
        public Task<Person?> GetPersonByEmail(Email email);

    }
}
