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
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.ToTable("PurchaseDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PurchaseDetailID").ValueGeneratedOnAdd();

            builder.Property(x => x.UnitCost).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Purchase)
                   .WithMany(p => p.PurchaseDetails)
                   .HasForeignKey(x => x.PurchaseID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Variant)
                   .WithMany()
                   .HasForeignKey(x => x.VariantID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
