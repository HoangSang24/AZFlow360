using AZFlow360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("UserID").ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);

            builder.HasOne(x => x.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(x => x.RoleID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
