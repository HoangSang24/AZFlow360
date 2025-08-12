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
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PromotionID").ValueGeneratedOnAdd();

            builder.Property(x => x.PromotionName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.TypeCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DiscountValue).HasColumnType("decimal(18,2)");
        }
    }
}
