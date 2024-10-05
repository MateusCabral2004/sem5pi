using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sempi5.Domain.User;

namespace Sempi5.Infrastructure.UserRepository
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Username).IsUnique();

            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(t => t.Email).IsUnique();

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Role)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
