using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.OperationTypeAggregate;

namespace Sempi5.Infrastructure.OperationTypeRepository
{
    public class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            // Configure the table name
            builder.ToTable("OperationType");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(s => s.Id)
                .HasColumnName("OperationTypeID")
                .IsRequired()
                .HasConversion(
                    s => s.AsLong(),
                    s => new OperationTypeID(s))
                .ValueGeneratedOnAdd();
            
            builder.Property(s => s.Name)
                .IsRequired()
                .HasConversion(
                    s => s.ToString(),
                    s => new OperationName(s))
                .HasMaxLength(100);
            
            builder.HasMany(s => s.RequiredStaff)
                .WithOne()  
                .HasForeignKey("OperationTypeID") 
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Name)
                .IsUnique();
            
            builder.Property(s => s.SetupDuration)
                .HasColumnName("SetupDuration")
                .HasConversion(
                    s => s.ToString(),       
                    s => TimeSpan.Parse(s)); 
            
            builder.Property(s => s.CleaningDuration)
                .HasColumnName("CleaningDuration")
                .HasConversion(
                    s => s.ToString(),       
                    s => TimeSpan.Parse(s)); 
            
            builder.Property(s => s.SurgeryDuration)
                .HasColumnName("SurgeryDuration")
                .HasConversion(
                    s => s.ToString(),       
                    s => TimeSpan.Parse(s)); 
            
        }
    }
}