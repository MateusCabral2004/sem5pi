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
            
            // Configure the primary key
            builder.HasKey(p => p.Id);
            
            // Configure the Id property
            builder.Property(s => s.Id)
                .HasColumnName("OperationTypeID")
                .IsRequired()
                .HasConversion(
                    s => s.AsLong(),
                    s => new OperationTypeID(s))
                .ValueGeneratedOnAdd();

            // Configure the Name property
            builder.Property(s => s.Name)
                .IsRequired()
                .HasConversion(
                    s => s.ToString(),
                    s => new OperationName(s))
                .HasMaxLength(100);

            // Configure the one-to-many relationship with RequiredStaff
            builder.HasMany(s => s.RequiredStaff)
                .WithOne()  // Assuming RequiredStaff does not have a navigation property to OperationType
                .HasForeignKey("OperationTypeID") // Foreign key in RequiredStaff
                .OnDelete(DeleteBehavior.Cascade); // Deleting OperationType cascades to RequiredStaff

            // Configure the Duration property
            builder.Property(s => s.Duration)
                .HasColumnName("Duration")
                .HasConversion(
                    s => s.ToString(),       // Convert TimeSpan to string for storage
                    s => TimeSpan.Parse(s)); // Convert back to TimeSpan when reading
        }
    }
}