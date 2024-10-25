using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.RequiredStaffRepository;

public interface IRequiredStaffRepository : IRepository<RequiredStaff, RequiredStaffID>
{
    
}