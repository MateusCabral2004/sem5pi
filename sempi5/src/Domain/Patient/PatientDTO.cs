using Sempi5.Domain.PersonalData;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient;

public class PatientDTO
{
    public string name{get; set;}
    public string email{get; set;}
    public string birthDate{get; set;}

    //  public SystemUser user = null;
    //   public Person Person { get; set; }
    // public DateTime BirthDate { get; set; }
    // public string Gender { get; set; }
    // public List<string> AllergiesAndMedicalConditions { get; set; }
    // public string EmergencyContact { get; set; }
    // public List<string> AppointmentHistory { get; set; }

}