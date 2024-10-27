using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.Shared;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PatientAggregate;
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

        public async Task<Patient> GetByPatientEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            
            var patient = await Task.Run(() => context.Patients
                .Include(p => p.Person)
                .AsEnumerable() //Permite que use metodos de c# 
                .FirstOrDefault(p => p.Person.ContactInfo.email().ToString().ToLower().Equals(email.ToLower())));

            return patient;

        }
        

        public async Task<Patient> GetByPhoneNumber(int phoneNumber)
        {
            if (phoneNumber <= 0)
            {
                return null;
            }

            var patient = await Task.Run(()=> context.Patients
                .Include(p => p.Person)  // Inclua a entidade 'Person' se não estiver sendo carregada automaticamente
                .ThenInclude(p => p.ContactInfo)  // Inclua 'ContactInfo' também
                .AsEnumerable()
                .FirstOrDefault(p => p.Person != null 
                                          && p.Person.ContactInfo != null 
                                          && p.Person.ContactInfo.phoneNumber().phoneNumber() == phoneNumber));

            return patient;
        }

        public async Task<Patient> GetActivePatientByMedicalRecordNumber(MedicalRecordNumber patientId)
        {
            if (patientId == null)
            {
                return null;
            }
            
            
            
                return context.Patients
                .Include(p => p.Person)
                .FirstOrDefault(p => p.Id.Equals(patientId) 
                                     && p.PatientStatus == PatientStatusEnum.ACTIVATED);
        }

        public async Task<Patient> GetByPatientId(string patientId)
        {
            
            if (string.IsNullOrEmpty(patientId))
            {
                return null;
            }


            var patient = context.Patients
                .Include(p => p.Person)
                .AsEnumerable()
                .FirstOrDefault(p => p.Id.AsString().Equals(patientId));

            return patient;
        }


        public async Task<List<Patient>> GetActivePatientsByName(Name name)
        {
            
            if (name == null)
            {
                return null;
            }

            return await context.Patients
                .Include(p => p.Person)
                .Where(p => p.Person.FullName.Equals(name) && p.PatientStatus == PatientStatusEnum.ACTIVATED)
                .ToListAsync();
        }
        
        public async Task<Patient> GetActivePatientByEmail(Email email)
        {
            
            if (email == null)
            {
                return null;
            }

            return context.Patients
                .Include(p => p.Person)
                .FirstOrDefault(p => p.Person.ContactInfo._email.Equals(email)
                && p.PatientStatus == PatientStatusEnum.ACTIVATED);
        }
        

        public async Task<List<Patient>> GetActivePatientsByDateOfBirth(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return null;
            }

            var patients = await context.Patients
                .Include((p => p.Person))
                .Where(p => p.BirthDate.Date == dateOfBirth.Value.Date
                && p.PatientStatus == PatientStatusEnum.ACTIVATED)
                .ToListAsync();

            return patients;
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

        public async Task<int> deleteExpiredEntitiesAsync(Patient patient)
        {
            DateTime today = DateTime.UtcNow;

            // Busca todos os pacientes que estão agendados para deletar e cujo prazo já passou
            var patientsToDelete = await context.Patients
                // .Where(p => p.deleteDate && p.deleteDate <= now)
                .ToListAsync();

            if (patientsToDelete.Count == 0)
            {
                return 0; // Nenhum paciente para deletar
            }

            // Remove os pacientes
            context.Patients.RemoveRange(patientsToDelete);
            return await context.SaveChangesAsync(); // Aplica a remoção no banco de dados
        }
    }
}