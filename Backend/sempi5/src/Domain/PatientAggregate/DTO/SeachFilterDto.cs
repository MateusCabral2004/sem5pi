namespace Sempi5.Domain.PatientAggregate;

public class SeachFilterDto
{
    public string? patientName { get; set; }
    public string? type { get; set; }
    public string? priority { get; set; }
    public string? status { get; set; }
}