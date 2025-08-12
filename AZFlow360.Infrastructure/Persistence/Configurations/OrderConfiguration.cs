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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("OrderID").ValueGeneratedOnAdd();

            builder.HasIndex(x => x.OrderCode).IsUnique();
            builder.Property(x => x.OrderCode).HasMaxLength(50).IsRequired();

            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(x => x.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(x => x.CustomerID)
                   .OnDelete(DeleteBehavior.SetNull); // Nếu khách hàng bị xóa, đơn hàng vẫn còn nhưng không thuộc về ai
        }
    }
}
