using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.PersonRepository
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v))
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v))
                .HasMaxLength(100);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Name(v))
                .HasMaxLength(200);
            
            builder.OwnsOne(p => p.ContactInfo, contactInfo =>
            {
                contactInfo.Property(ci => ci._email)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasConversion(
                        email => email.ToString(),
                        email => new Email(email))
                    .HasMaxLength(100);

                contactInfo.HasIndex(c => c._phoneNumber)
                    .IsUnique();
                
                contactInfo.Property(ci => ci._phoneNumber)
                    .HasColumnName("PhoneNumber")
                    .IsRequired()
                    .HasConversion(
                        phone => phone.phoneNumber(), // Assuming this is how you get the string representation
                        phone => new PhoneNumber(phone));
                
                contactInfo.HasIndex(ci => ci._email)
                    .IsUnique();
            });
        }
    }
}
