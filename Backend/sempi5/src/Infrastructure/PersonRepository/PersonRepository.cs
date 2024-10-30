using Microsoft.EntityFrameworkCore;
using Sempi5.Domain;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PersonAggregate;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.PersonRepository
{
    public class PersonRepository : BaseRepository<Person,PersonId>, IPersonRepository
    {
        
        private readonly DBContext context;
        
        public PersonRepository(DBContext dbContext) : base(dbContext.Person)
        {
            this.context = dbContext;
        }

        public Task<Person?> GetPersonByPhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
            {
                return null;
            }
            
            return context.Person.FirstOrDefaultAsync(p => p.ContactInfo._phoneNumber.Equals(phoneNumber));
        }

        public Task<Person?> GetPersonByEmail(Email email)
        {
            if (email == null)
            {
                return null;
            }
            
            
            return context.Person.FirstOrDefaultAsync(p => p.ContactInfo._email.Equals(email));
        }

        public async Task RemoveAsync(Person person)
        { 
            context.Person.Remove(person);
        }
    }
}