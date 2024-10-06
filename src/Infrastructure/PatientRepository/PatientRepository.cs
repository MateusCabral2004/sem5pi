using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientRepository : IPatientRepository
    {

        private readonly DBContext context;

        public PatientRepository(DBContext context)
        {
            this.context = context;
        }

        public Task<Patient> AddAsync(Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
            return Task.FromResult(patient);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await context.Patients.Include(u => u.User).ToListAsync();
        }

        public async Task<Patient> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var patient = await Task.Run(() => context.Patients
                .Include(p => p.User)
                .FirstOrDefault(p => p.User.Email.ToLower().Equals(email.ToLower())));

            return patient;
        }

        public Task<Patient> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetByIdsAsync(List<long> ids)
        {
            throw new NotImplementedException();
        }

        public void Remove(Patient obj)
        {
            throw new NotImplementedException();
        }
    }
}