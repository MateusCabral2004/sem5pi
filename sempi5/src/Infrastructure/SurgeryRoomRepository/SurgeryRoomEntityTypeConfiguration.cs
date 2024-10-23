using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain;
using Sempi5.Domain.SurgeryRoomAggregate;
using Sempi5.Domain.TodoItem;

namespace Sempi5.Infrastructure.SurgeryRoomRepository
{
    public class SurgeryRoomEntityTypeConfiguration : IEntityTypeConfiguration<SurgeryRoom>
    {
        public void Configure(EntityTypeBuilder<SurgeryRoom> builder)
        {
            builder.ToTable("SurgeryRooms");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.AsString(),  
                    v => new RoomNumber() 
                );

            builder.Property(t => t.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (RoomTypeEnum)Enum.Parse(typeof(RoomTypeEnum), v)
                );

            builder.Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (RoomTypeEnum)Enum.Parse(typeof(RoomTypeEnum), v)
                );

            builder.Property(t => t.Capacity)
                .HasConversion(
                    v => v.AsInt(),
                    v => new RoomCapacity(v)
                );
        }
    }
}
