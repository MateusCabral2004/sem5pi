using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Specialization;

namespace Sempi5.Infrastructure.SpecializationRepository;

public class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("Specialization");

        builder.HasKey(p => p.Id);

        builder.Property(s => s.specializationName)
            .IsRequired()
            .HasConversion(
                s => s.ToString(),
                s => new SpecializationName(s)
            )
            .HasMaxLength(100);
    }
}