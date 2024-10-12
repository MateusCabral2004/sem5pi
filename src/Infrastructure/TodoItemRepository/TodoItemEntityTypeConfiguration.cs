using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain;
using Sempi5.Domain.TodoItem;

namespace Sempi5.Infrastructure.TodoItemRepository
{
    public class TodoItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("TodoItems");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .HasConversion(
                    v => v.AsString(),  // Store the ID as a string in the database
                    v => new TodoItemId(v)  // Convert the string back to TodoItemId when reading
                );
            
            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.IsComplete)
                .HasDefaultValue(false);
        }
    }
}
