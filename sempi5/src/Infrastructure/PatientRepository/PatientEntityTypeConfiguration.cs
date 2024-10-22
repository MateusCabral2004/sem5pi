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
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new PatientId(v)
                )
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Patient>("UserId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
