using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientRepository : BaseRepository<Patient,MedicalRecordNumber>, IPatientRepository
    {

        private readonly DBContext context;
        
        public PatientRepository(DBContext context) : base(context.Patients)
        { this.context = context; }

        public async Task<Patient> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var patient = await Task.Run(() => context.Patients
                .Include(p => p.User)
                .FirstOrDefault(p => p.User.Email.ToString().ToLower().Equals(email.ToLower())));

            return patient;
        }

        public Task<Patient> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetByMedicalRecordNumber(string medicalRecordNumber)
        {
            throw new NotImplementedException();
        }
    }
}