using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.Staff;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.StaffRepository
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(t => t.LicenseNumber).IsUnique();

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Specialization)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Password)
                .HasMaxLength(200);

            /*
            builder.Property(t => t.AvailabilitySlots)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
            .HasMaxLength(200);
            */
            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Staff>("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
