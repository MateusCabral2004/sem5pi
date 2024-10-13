using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientRepository : BaseRepository<Patient,PatientId>, IPatientRepository
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
                .FirstOrDefault(p => p.User.Email.ToLower().Equals(email.ToLower())));

            return patient;
        }
        
    }
}