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
                .AsEnumerable() //Permite que use metodos de c# 
                .FirstOrDefault(p => p.User.Email.ToString().ToLower().Equals(email.ToLower())));

            return patient;
        }

        public async Task<Patient> GetByName(string name)
        {
            
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            
            var patient = context.Patients
                .Include(p => p.User)
                .FirstOrDefault(p => p.Person.FullName.ToString().ToLower().Equals(name.ToLower()));

            return patient;
        }

        public Task<Patient> GetByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetByMedicalRecordNumber(string medicalRecordNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            var patient = await context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Person.ContactInfo.phoneNumber().Equals((phoneNumber)));

            return patient;
        }

        public async Task SavePatientAsync(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            context.Patients.Update(patient);
            await context.SaveChangesAsync();
        }
    }
}