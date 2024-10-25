using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.OperationRequestAggregate;

namespace Sempi5.Infrastructure.OperationRequestRepository;

public class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest>
{
    public void Configure(EntityTypeBuilder<OperationRequest> builder)
    {
        builder.ToTable("OperationRequest");

        builder.HasKey(p => p.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("OperationTypeID")
            .IsRequired()
            .HasConversion(
                s => s.AsLong(),
                s => new OperationRequestID(s))
            .ValueGeneratedOnAdd();

        builder.HasOne(s => s.Doctor)
            .WithOne()
            .HasForeignKey<OperationRequest>("DoctorID")
            .IsRequired();
        
        builder.HasOne(s => s.Patient)
            .WithOne()
            .HasForeignKey<OperationRequest>("PatientID")
            .IsRequired();
        
        builder.HasOne(s => s.OperationType)
            .WithOne()
            .HasForeignKey<OperationRequest>("OperationTypeID")
            .IsRequired();
        
        builder.Property(p => p.DeadLineDate)
            .HasColumnName("DeadLineDate")
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(p => p.PriorityEnum)
            .IsRequired()
            .HasConversion(
                p => p.ToString(),
                p => (PriorityEnum)Enum.Parse(typeof(PriorityEnum), p)
            );

    }
}