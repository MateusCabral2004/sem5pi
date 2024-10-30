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
            .HasColumnName("OperationRequestID")
            .IsRequired()
            .HasConversion(
                s => s.AsLong(),
                s => new OperationRequestID(s))
            .ValueGeneratedOnAdd();

        builder.HasOne(s => s.Doctor)
            .WithMany()
            .HasForeignKey("DoctorID")
            .IsRequired();
        
        builder.HasOne(s => s.Patient)
            .WithMany()
            .HasForeignKey("PatientID")
            .IsRequired();
        
        builder.HasOne(s => s.OperationType)
            .WithMany()
            .HasForeignKey("OperationTypeID")
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