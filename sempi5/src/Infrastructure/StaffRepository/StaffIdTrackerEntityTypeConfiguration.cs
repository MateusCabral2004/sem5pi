using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.Staff;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffIdTrackerEntityTypeConfiguration : IEntityTypeConfiguration<StaffIdTracker>
    {
        public void Configure(EntityTypeBuilder<StaffIdTracker> builder)
        {
            builder.ToTable("StaffIdTracker");
            builder.HasKey(p => p.year);

            builder.Property(p => p.year)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(p => p.nextIdToUse)
                .IsRequired();
        }
    }
}