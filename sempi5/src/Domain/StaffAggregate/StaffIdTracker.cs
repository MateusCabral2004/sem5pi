using Sempi5.Domain.Shared;

namespace Sempi5.Domain.StaffAggregate;

public class StaffIdTracker
{
    public string year { get; set; }
    public int nextIdToUse { get; set; }
}