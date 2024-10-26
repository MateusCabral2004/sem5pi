using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.PatientAggregate;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.HasKey(p => p.Id);

            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Patient>("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new MedicalRecordNumber(v)
                )
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Person)
                .WithOne()
                .HasForeignKey<Patient>("PersonId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.BirthDate)
                .HasColumnType("DATE")
                .IsRequired();
            
            builder.Property(p => p.PatientStatus)
                .IsRequired()
                .HasConversion(
                    p => p.ToString(),
                    p => (PatientStatusEnum)Enum.Parse(typeof(PatientStatusEnum), p)
                );
            
        }
    }
}