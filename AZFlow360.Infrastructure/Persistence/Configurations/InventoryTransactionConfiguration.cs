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
    public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("TransactionID").ValueGeneratedOnAdd();

            builder.HasOne(x => x.Variant)
                   .WithMany() // Giả sử Variant không cần biết về danh sách transaction
                   .HasForeignKey(x => x.VariantID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.TransactionType).HasMaxLength(50).IsRequired();
            builder.Property(x => x.QuantityChange).IsRequired();
        }
    }
}