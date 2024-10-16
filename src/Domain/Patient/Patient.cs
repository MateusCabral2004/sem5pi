using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {

        //Class can and should be modified so that fields are more representative 
        //of what they are instead of just strings
        public PatientId Id { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string ContactInfo { get; set; }
        public List<string> AllergiesAndMedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
        
        public Boolean IsConfirmed { get; set; }
        public SystemUser User { get; set; }        
        public Patient(string firstName, string email, string medicalRecordNumber,Boolean isConfirmed)
        {
            FirstName = firstName;
            ContactInfo = email;
            MedicalRecordNumber = medicalRecordNumber;
            IsConfirmed = isConfirmed;
            AppointmentHistory = new List<string>(); // Initialize appointment history
        }

        public Patient()
        {
            throw new NotImplementedException();
        }
    }
}