using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient
{
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {

        //Class can and should be modified so that fields are more representative 
        //of what they are instead of just strings
        public SystemUser User { get; set; }       
        public MedicalRecordNumber Id { get; set; }
        public Person Person { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public List<string> AllergiesAndMedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
        
    }
}