using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.PatientAggregate;

namespace Sempi5.Infrastructure.PatientRepository
{
    public class PatientIdTrackerEntityTypeConfiguration : IEntityTypeConfiguration<PatientIdTracker>
    {
        public void Configure(EntityTypeBuilder<PatientIdTracker> builder)
        {
            builder.ToTable("PatientIdTracker");
            builder.HasKey(p => p.yearMonth);

            builder.Property(p => p.yearMonth)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(p => p.nextIdToUse)
                .IsRequired();
        }
    }
}