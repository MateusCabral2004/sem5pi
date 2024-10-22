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
            builder.ToTable("Patient");
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new MedicalRecordNumber(v)
                )
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Patient>("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Id)
                .HasConversion(
                    p => p.AsString(),
                    p => new MedicalRecordNumber(p)
                )
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .HasColumnType("DATE")
                .IsRequired();         

        }
    }
}
