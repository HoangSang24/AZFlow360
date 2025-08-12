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
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("VariantID").ValueGeneratedOnAdd();

            builder.HasIndex(x => x.SKU).IsUnique();
            builder.Property(x => x.SKU).HasMaxLength(100).IsRequired();

            builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0m);
            builder.Property(x => x.SalePrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Stock).HasDefaultValue(0);

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.Variants)
                   .HasForeignKey(x => x.ProductID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
