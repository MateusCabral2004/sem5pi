using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.OperationRequest;
using Sempi5.Domain.OperationType;
using Sempi5.Domain.Staff;

namespace Sempi5.Infrastructure.OperationRequest;

public class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<Domain.OperationRequest.OperationRequest>
{
    public void Configure(EntityTypeBuilder<Domain.OperationRequest.OperationRequest> builder)
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
            .HasForeignKey<Domain.OperationRequest.OperationRequest>("DoctorID")
            .IsRequired();
        
        builder.HasOne(s => s.Patient)
            .WithOne()
            .HasForeignKey<Domain.OperationRequest.OperationRequest>("PatientID")
            .IsRequired();
        
        builder.HasOne(s => s.OperationType)
            .WithOne()
            .HasForeignKey<Domain.OperationRequest.OperationRequest>("OperationTypeID")
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