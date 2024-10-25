namespace Sempi5.Domain.PatientAggregate;

public class PatientRecordDTO
{
    public string id { get; set; }
    public string name { get; set; }
    public List<string> appointments { get; set; } 
    
}