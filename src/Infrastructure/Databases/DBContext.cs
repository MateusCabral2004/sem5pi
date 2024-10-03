using Microsoft.EntityFrameworkCore;
using Sempi5.Infrastructure.TodoItemRepository;

namespace Sempi5.Domain.TodoItem
{
    public class DBContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TodoItemEntityTypeConfiguration());
        }
        
    }
}