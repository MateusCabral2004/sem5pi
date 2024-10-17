using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
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
            
            builder.Property(t => t.Id)
                .HasConversion(
                    v => v.AsString(),
                    v => new StaffId(v)
                )
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v)
                    )
                .HasMaxLength(100);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v)
                    )
                .HasMaxLength(100);

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v)
                    )
                .HasMaxLength(200);

            builder.Property(t => t.Specialization)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(t => t.LicenseNumber)
                .IsRequired()
                .HasConversion(
                    v => v.licenseNumber(),
                    v => new LicenseNumber(v)
                );
            
            builder.OwnsOne(t => t.ContactInfo, contactInfo =>
            {
                contactInfo.Property(ci => ci._email)
                    .HasColumnName("Email") // Nome da coluna para o email
                    .IsRequired()
                    .HasConversion(
                        ci => ci.ToString(),
                        ci => new Email(ci))
                    .HasMaxLength(100);

                contactInfo.Property(ci => ci._phoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasConversion(
                        ci => ci.phoneNumber(),
                        ci => new PhoneNumber(ci)
                        )
                    .IsRequired();
            });

            
            
            
            builder.HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Staff>("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
