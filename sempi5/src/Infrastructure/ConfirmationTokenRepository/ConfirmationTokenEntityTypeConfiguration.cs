using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.ConfirmationTokenRepository
{
    public class ConfirmationTokenEntityTypeConfiguration : IEntityTypeConfiguration<ConfirmationToken>
    {
        public void Configure(EntityTypeBuilder<ConfirmationToken> builder)
        {
            builder.ToTable("ConfirmationTokens");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.email)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => new Email(v))
                .HasMaxLength(100);
            
            builder.HasIndex(p => p.email)
                .IsUnique();

            builder.Property(p => p.ExpiryDate)
                .IsRequired();

            builder.Property(p => p.IsUsed)
                .IsRequired();

            builder.Property(p => p.IdToAssociate)
                .IsRequired();

        }
    }
}