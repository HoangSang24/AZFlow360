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
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PurchaseID").ValueGeneratedOnAdd();

            builder.HasIndex(x => x.PurchaseCode).IsUnique();
            builder.Property(x => x.PurchaseCode).HasMaxLength(50).IsRequired();

            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Supplier)
                   .WithMany(s => s.Purchases)
                   .HasForeignKey(x => x.SupplierID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Purchases)
                   .HasForeignKey(x => x.UserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
