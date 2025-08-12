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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("CustomerID").ValueGeneratedOnAdd();

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();

            builder.HasIndex(x => x.Phone)
                   .IsUnique()
                   .HasFilter("[Phone] IS NOT NULL"); // Chỉ áp dụng unique cho các giá trị không null
        }
    }
}
