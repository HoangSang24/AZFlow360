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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("OrderDetailID").ValueGeneratedOnAdd();

            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(x => x.OrderID)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa chi tiết khi đơn hàng bị xóa

            builder.HasOne(x => x.Variant)
                   .WithMany() // Giả sử Variant không cần biết về OrderDetails
                   .HasForeignKey(x => x.VariantID)
                   .OnDelete(DeleteBehavior.Restrict); // Không cho xóa biến thể nếu còn trong chi tiết đơn hàng
        }
    }
}
