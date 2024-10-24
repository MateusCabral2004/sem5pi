using Sempi5.Domain.PersonalData;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient;

public class PatientDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string? fullName { get; set; }
    public string? email { get; set; }
    public string birthDate { get; set; }
    public int? phoneNumber { get; set; }
    public string gender { get; set; }
    public List<string>? allergiesAndMedicalConditions { get; set; }
    public string emergencyContact { get; set; }
    public List<string>? appointmentHistory { get; set; }
}