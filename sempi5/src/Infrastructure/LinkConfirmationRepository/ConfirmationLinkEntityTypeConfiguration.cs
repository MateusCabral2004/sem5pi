using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.ConfirmationLink;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.LinkConfirmationRepository;

public class ConfirmationLinkEntityTypeConfiguration : IEntityTypeConfiguration<ConfirmationLink>
{
    public void Configure(EntityTypeBuilder<ConfirmationLink> builder)
    {
        builder.ToTable("Confirmation-Link");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.email)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => new Email(v))
            .HasMaxLength(100);
            
        builder.Property(p => p.ExpiryDate)
            .IsRequired();

        builder.Property(p => p.IsUsed)
            .IsRequired();

    }

  
}