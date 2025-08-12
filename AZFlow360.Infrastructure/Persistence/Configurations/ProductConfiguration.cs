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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ProductID").ValueGeneratedOnAdd();

            builder.Property(x => x.ProductName).HasMaxLength(255).IsRequired();

            builder.HasOne(x => x.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(x => x.CategoryID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Supplier)
                   .WithMany(s => s.Products)
                   .HasForeignKey(x => x.SupplierID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}