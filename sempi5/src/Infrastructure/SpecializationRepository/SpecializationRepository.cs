using Sempi5.Domain.Specialization;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.SpecializationRepository
{
    public class SpecializationRepository : BaseRepository<Specialization, SpecializationID>, ISpecializationRepository
    {
        private readonly DBContext context;
        
        public SpecializationRepository(DBContext dbContext) : base(dbContext.Specializations)
        {
            this.context = dbContext;
        }
        
        
    }
    
    
}

