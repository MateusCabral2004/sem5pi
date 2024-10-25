using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.SpecializationAggregate;
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

    public Task<RequiredStaff> GetByRequiredStaff(RequiredStaff requiredStaff)
    {
        if(requiredStaff == null)
        {
            return null;
        }

        var name = requiredStaff.Specialization;
        var number = requiredStaff.NumberOfStaff;
        
        return context.RequiredStaffs.FirstOrDefaultAsync(p => p.Specialization.Equals(name) && p.NumberOfStaff.Equals(number));
    }
    
}