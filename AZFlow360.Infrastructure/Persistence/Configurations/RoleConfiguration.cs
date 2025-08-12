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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RoleID");

            builder.HasIndex(x => x.RoleName).IsUnique();
            builder.Property(x => x.RoleName).HasMaxLength(50).IsRequired();

            // Dữ liệu mẫu bây giờ sẽ hoạt động bình thường
            builder.HasData(
                new { Id = 1, RoleName = "Administrator" },
                new { Id = 2, RoleName = "Manager" },
                new { Id = 3, RoleName = "Salesperson" }
            );
        }
    }
}
