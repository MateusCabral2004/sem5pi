namespace Sempi5.Domain.Patient;

public class PatientProfileDto
{
    
    public string email { get; set; }
    public int phoneNumber { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime birthDate { get; set; }
    public string gender { get; set; }
    public List<string> allergiesAndMedicalConditions { get; set; }
    public string emergencyContact { get; set; }
    public List<string> appointmentHistory { get; set; }

}