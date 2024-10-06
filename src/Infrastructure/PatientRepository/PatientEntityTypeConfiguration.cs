using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.MedicalRecordNumber);

            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Patient>("UserEmail")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
