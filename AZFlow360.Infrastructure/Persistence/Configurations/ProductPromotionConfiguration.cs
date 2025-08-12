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
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            builder.ToTable("ProductPromotions");

            // Khóa chính kép
            builder.HasKey(pp => new { pp.ProductID, pp.PromotionID });

            builder.HasOne(pp => pp.Product)
                   .WithMany(p => p.ProductPromotions)
                   .HasForeignKey(pp => pp.ProductID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.Promotion)
                   .WithMany(pr => pr.ProductPromotions)
                   .HasForeignKey(pp => pp.PromotionID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
