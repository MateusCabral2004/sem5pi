using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.AppointmentAggregate;
using Sempi5.Domain.Shared;

namespace Sempi5.Infrastructure.AppointmentRepository;

public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        
        builder.ToTable("Appointment");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasColumnName("AppointmentID")
            .IsRequired()
            .HasConversion(
                v => v.AsLong(),
                v => new AppointmentID(v))
            .ValueGeneratedOnAdd();

        builder.HasOne(t => t.OperationRequest)
            .WithOne()
            .HasForeignKey<Appointment>("OperationRequestID");
        
        builder.HasOne(t => t.SurgeryRoom)
            .WithOne()
            .HasForeignKey<Appointment>("SurgeryRoomID")
            .IsRequired();
        
        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion(
                p => p.ToString(),
                p => (StatusEnum)Enum.Parse(typeof(StatusEnum), p)
            );

        builder.Property(p => p.Date)
            .HasColumnName("Date")
            .HasColumnType("DATE")
            .IsRequired();

        
    }
}