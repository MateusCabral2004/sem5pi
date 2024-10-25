using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.AccoutToDelete;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.AccoutToDeleteRepository;

public class AccountToDeleteEntityTypeConfiguration : IEntityTypeConfiguration<AccoutToDelete>
{
    public void Configure(EntityTypeBuilder<AccoutToDelete> builder)
    {
        builder.ToTable("AccountToDelete");
        builder.HasKey(a=>a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("AccountToDeleteID")
            .IsRequired()
            .HasConversion(
                v => v.AsLong(),
                v => new SystemUserId(v));
        
        builder.Property(a=>a.DateToDelete)
            .HasColumnName("DateToDelete")
            .HasColumnType("DATE")
            .IsRequired();
    }
}