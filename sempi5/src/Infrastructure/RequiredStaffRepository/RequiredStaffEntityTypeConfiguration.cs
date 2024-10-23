using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.OperationType;
using Sempi5.Domain.Specialization;

namespace Sempi5.Infrastructure.RequiredStaffRepository;

public class RequiredStaffEntityTypeConfiguration : IEntityTypeConfiguration<RequiredStaff>
{
    public void Configure(EntityTypeBuilder<RequiredStaff> builder)
    {
        builder.ToTable("RequiredStaff");

        builder.HasKey(p => p.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("RequiredStaffID")
            .IsRequired()
            .HasConversion(
                s => s.AsLong(),
                s => new RequiredStaffID(s))
            .ValueGeneratedOnAdd();

        builder.Property(r => r.NumberOfStaff)
            .HasColumnName("NumberOfStaff")
            .HasConversion(
                r => r.getValue(),
                r => new NumberOfStaff(r));

    }
}