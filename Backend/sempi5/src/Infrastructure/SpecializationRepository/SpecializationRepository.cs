using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.SpecializationAggregate;

namespace Sempi5.Infrastructure.SpecializationRepository
{
    public class SpecializationRepository : BaseRepository<Specialization, SpecializationID>, ISpecializationRepository
    {
        private readonly DBContext context;
        
        public SpecializationRepository(DBContext dbContext) : base(dbContext.Specializations)
        {
            this.context = dbContext;
        }


        public async Task<Specialization?> GetBySpecializationName(Specialization specialization)
        {
            if(specialization == null)
            {
                return null;
            }

            var specializationSearched = await Task.Run(() => context.Specializations
                .FirstOrDefault(p => p.specializationName.Equals(specialization.specializationName)));

            return specializationSearched;
        }
    }
    
    
}

