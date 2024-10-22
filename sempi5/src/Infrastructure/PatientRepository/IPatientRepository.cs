using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.PatientRepository
{
    public interface IPatientRepository : IRepository<Patient,MedicalRecordNumber>
    {
        public Task<Patient> GetByEmail(string email);
        
        public Task<Patient> GetByName(string name);
        
        public Task<Patient> GetByDateOfBirth(DateTime dateOfBirth);
        
        public Task<Patient> GetByMedicalRecordNumber(string medicalRecordNumber);
        public Task SavePatientAsync(Patient patient);
        public Task<Patient> GetByPhoneNumber(string phoneNumber);
    }
}
