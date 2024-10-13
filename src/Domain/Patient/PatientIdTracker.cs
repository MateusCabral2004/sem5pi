using Sempi5.Domain.Shared;

namespace Sempi5.Domain.Patient;

public class PatientIdTracker
{
    public string yearMonth { get; set; }
    public int nextIdToUse { get; set; }
}