using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Staff;
using Sempi5.Domain.TodoItem;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Infrastructure.TodoItemRepository;
using Sempi5.Infrastructure.UserRepository;

namespace Sempi5.Infrastructure.Databases
{
    public class DBContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<SystemUser> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TodoItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
        }
        
    }
}