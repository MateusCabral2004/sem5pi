using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.PatientRepository
{
    public interface IPatientRepository : IRepository<Patient, MedicalRecordNumber>
    {
        public Task<Patient> GetByEmail(string email);

        public Task<Patient> GetActivePatientByEmail(Email email);
        
        public Task<List<Patient>> GetActivePatientsByName(Name name);

        public Task<List<Patient>> GetActivePatientsByDateOfBirth(DateTime? dateOfBirth);
        
        public Task SavePatientAsync(Patient patient);

        public Task<Patient> GetByPhoneNumber(int phoneNumber);

        public Task<Patient> GetActivePatientByMedicalRecordNumber(MedicalRecordNumber patientId);
        
        public Task<Patient> GetByPatientId(string patientId);
        
        public Task<int> deleteExpiredEntitiesAsync(Patient patient);
    }
}