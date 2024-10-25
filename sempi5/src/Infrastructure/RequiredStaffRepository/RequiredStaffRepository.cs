using Microsoft.CodeAnalysis.Elfie.Serialization;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.RequiredStaffRepository;

public class RequiredStaffRepository : BaseRepository<RequiredStaff, RequiredStaffID>, IRequiredStaffRepository
{
    private readonly DBContext context;
    
    public RequiredStaffRepository(DBContext dbContext) : base(dbContext.RequiredStaffs)
    {
        this.context = dbContext;
    }

}