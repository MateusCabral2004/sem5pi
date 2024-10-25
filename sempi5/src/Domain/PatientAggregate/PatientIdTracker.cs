using Sempi5.Domain.Shared;

namespace Sempi5.Domain.PatientAggregate;

public class PatientIdTracker
{
    public string yearMonth { get; set; }
    public int nextIdToUse { get; set; }
}