using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Staff;

public class StaffIdTracker
{
    public string year { get; set; }
    public int nextIdToUse { get; set; }
}