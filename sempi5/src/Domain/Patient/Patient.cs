using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Domain.Patient
{
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {

        //Class can and should be modified so that fields are more representative 
        //of what they are instead of just strings
        public SystemUser? User { get; set; }      
        public MedicalRecordNumber Id { get; set; }
        public Person Person { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public List<string> AllergiesAndMedicalConditions { get; set; }
        public string EmergencyContact { get; set; }
        public List<string> AppointmentHistory { get; set; }
        public MedicalRecordStatusEnum MedicalRecordStatus { get; set; }

        private Patient() {}
        
        public Patient(SystemUser user, Person person, DateTime birthDate, string gender, List<string> allergiesAndMedicalConditions, string emergencyContact, List<string> appointmentHistory)
        {
            User = user;
            Person = person;
            BirthDate = birthDate;
            Gender = gender;
            AllergiesAndMedicalConditions = allergiesAndMedicalConditions;
            EmergencyContact = emergencyContact;
            AppointmentHistory = appointmentHistory;
            MedicalRecordStatus = MedicalRecordStatusEnum.ACTIVATED;
        }
        
        public Patient(SystemUser user, Person person, DateTime birthDate, string gender, List<string> allergiesAndMedicalConditions, string emergencyContact, List<string> appointmentHistory, MedicalRecordStatusEnum medicalRecordStatus)
        {
            User = user;
            Person = person;
            BirthDate = birthDate;
            Gender = gender;
            AllergiesAndMedicalConditions = allergiesAndMedicalConditions;
            EmergencyContact = emergencyContact;
            AppointmentHistory = appointmentHistory;
            MedicalRecordStatus = medicalRecordStatus;
        }

        public Patient(SystemUser user, MedicalRecordNumber id, Person person, DateTime birthDate, string gender, List<string> allergiesAndMedicalConditions, string emergencyContact)
        {
            User = user;
            Person = person;
            BirthDate = birthDate;
            Gender = gender;
            AllergiesAndMedicalConditions = allergiesAndMedicalConditions;
            EmergencyContact = emergencyContact;
            MedicalRecordStatus = MedicalRecordStatusEnum.ACTIVATED;
        }
    }
}