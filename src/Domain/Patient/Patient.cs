using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient
{
    public class Patient
    {

        //Class can and should be modified so that fields are more representative 
        //of what they are instead of just strings
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string ContactInfo { get; set; }
        public List<string> AllergiesAndMedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
        public SystemUser User { get; set; }        

        public Patient(string firstName, string lastName, string fullName,
            string birthDate, string gender, string medicalRecordNumber,
            string contactInfo, List<string> allergiesAndMedicalConditions,
            string emergencyContact, List<string> appointmentHistory,
            string email, string role)
        {
            User = new SystemUser(email, role);
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            MedicalRecordNumber = medicalRecordNumber;
            ContactInfo = contactInfo;
            AllergiesAndMedicalConditions = allergiesAndMedicalConditions;
            EmergencyContact = emergencyContact;
            AppointmentHistory = appointmentHistory;
        }
    }
}