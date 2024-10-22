using System.Collections.Immutable;
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
                .Include(p => p.Person)
                .AsEnumerable() //Permite que use metodos de c# 
                .FirstOrDefault(p => p.User.Email.ToString().ToLower().Equals(email.ToLower())));

            return patient;
        }

        public async Task<Patient?> GetByName(string name)
        {
            
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            
            var patient = context.Patients
                .Include(p => p.Person)
                .AsEnumerable()
                .FirstOrDefault(p => p.Person.FullName._name.ToLower().Equals(name.ToLower()));

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetByDateOfBirth(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return Enumerable.Empty<Patient>();  // Retorna uma lista vazia se a data for null
            }

            var patients = await context.Patients
                .Include((p => p.Person))
                .Where(p => p.BirthDate.Date == dateOfBirth.Value.Date)
                .ToListAsync();

            return patients;
        }


        public Task<Patient> GetByMedicalRecordNumber(string medicalRecordNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient?> GetByPhoneNumber(string phoneNumber)
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