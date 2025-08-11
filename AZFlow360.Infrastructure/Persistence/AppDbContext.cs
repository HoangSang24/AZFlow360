using AZFlow360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<ProductPromotion> ProductPromotions => Set<ProductPromotion>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
        public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Roles
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(x => x.RoleID);
                b.HasIndex(x => x.RoleName).IsUnique();
            });

            // Users
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.UserID);
                b.HasIndex(x => x.Username).IsUnique();
                b.Property(x => x.Username).HasMaxLength(100).IsRequired();
                b.Property(x => x.FullName).HasMaxLength(100).IsRequired();
                b.HasOne(x => x.Role).WithMany(r => r.Users).HasForeignKey(x => x.RoleID).OnDelete(DeleteBehavior.Restrict);
                b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Categories (self reference)
            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(x => x.CategoryID);
                b.HasIndex(x => x.CategoryName).IsUnique();
                b.Property(x => x.CategoryName).HasMaxLength(100).IsRequired();
                b.HasOne(x => x.ParentCategory).WithMany(x => x.Children).HasForeignKey(x => x.ParentCategoryID).OnDelete(DeleteBehavior.Restrict);
            });

            // Suppliers
            modelBuilder.Entity<Supplier>(b =>
            {
                b.HasKey(x => x.SupplierID);
                b.Property(x => x.SupplierName).HasMaxLength(255).IsRequired();
            });

            // Products
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(x => x.ProductID);
                b.Property(x => x.ProductName).HasMaxLength(255).IsRequired();
                b.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryID).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Supplier).WithMany(s => s.Products).HasForeignKey(x => x.SupplierID).OnDelete(DeleteBehavior.SetNull);
            });

            // ProductVariants
            modelBuilder.Entity<ProductVariant>(b =>
            {
                b.HasKey(x => x.VariantID);
                b.HasIndex(x => x.SKU).IsUnique();
                b.Property(x => x.SKU).HasMaxLength(100).IsRequired();
                b.Property(x => x.CostPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0m);
                b.Property(x => x.SalePrice).HasColumnType("decimal(18,2)").IsRequired();
                b.Property(x => x.Stock).HasDefaultValue(0);
                b.HasOne(x => x.Product).WithMany(p => p.Variants).HasForeignKey(x => x.ProductID).OnDelete(DeleteBehavior.Cascade);
                b.Property(x => x.RowVersion).IsRowVersion();
            });

            // Customers
            modelBuilder.Entity<Customer>(b =>
            {
                b.HasKey(x => x.CustomerID);
                b.Property(x => x.FullName).HasMaxLength(100).IsRequired();
                b.HasIndex(x => x.Phone).IsUnique().HasFilter("[Phone] IS NOT NULL");
            });

            // Promotions
            modelBuilder.Entity<Promotion>(b =>
            {
                b.HasKey(x => x.PromotionID);
                b.Property(x => x.PromotionName).HasMaxLength(255).IsRequired();
                b.Property(x => x.TypeCode).HasMaxLength(50).IsRequired();
                b.Property(x => x.DiscountValue).HasColumnType("decimal(18,2)");
            });

            // Orders
            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(x => x.OrderID);
                b.HasIndex(x => x.OrderCode).IsUnique();
                b.Property(x => x.OrderCode).HasMaxLength(50).IsRequired();
                b.HasOne(x => x.User).WithMany(u => u.Orders).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Customer).WithMany(c => c.Orders).HasForeignKey(x => x.CustomerID).OnDelete(DeleteBehavior.SetNull);
            });

            // OrderDetails
            modelBuilder.Entity<OrderDetail>(b =>
            {
                b.HasKey(x => x.OrderDetailID);
                b.HasOne(x => x.Order).WithMany(o => o.OrderDetails).HasForeignKey(x => x.OrderID).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.Variant).WithMany(v => v.OrderDetails).HasForeignKey(x => x.VariantID).OnDelete(DeleteBehavior.Restrict);
                b.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            });

            // Purchases
            modelBuilder.Entity<Purchase>(b =>
            {
                b.HasKey(x => x.PurchaseID);
                b.HasIndex(x => x.PurchaseCode).IsUnique();
                b.Property(x => x.PurchaseCode).HasMaxLength(50).IsRequired();
                b.HasOne(x => x.Supplier).WithMany(s => s.Purchases).HasForeignKey(x => x.SupplierID).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.User).WithMany(u => u.Purchases).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
            });

            // PurchaseDetails
            modelBuilder.Entity<PurchaseDetail>(b =>
            {
                b.HasKey(x => x.PurchaseDetailID);
                b.HasOne(x => x.Purchase).WithMany(p => p.PurchaseDetails).HasForeignKey(x => x.PurchaseID).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.Variant).WithMany(v => v.PurchaseDetails).HasForeignKey(x => x.VariantID).OnDelete(DeleteBehavior.Restrict);
                b.Property(x => x.UnitCost).HasColumnType("decimal(18,2)");
            });

            // InventoryTransactions
            modelBuilder.Entity<InventoryTransaction>(b =>
            {
                b.HasKey(x => x.TransactionID);
                b.HasOne(x => x.Variant).WithMany(v => v.InventoryTransactions).HasForeignKey(x => x.VariantID).OnDelete(DeleteBehavior.Restrict);
                b.Property(x => x.TransactionType).HasMaxLength(50).IsRequired();
                b.Property(x => x.QuantityChange).IsRequired();
            });

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Administrator" },
                new Role { RoleID = 2, RoleName = "Manager" },
                new Role { RoleID = 3, RoleName = "Salesperson" }
            );

            modelBuilder.Entity<ProductPromotion>(b =>
            {
                // Khóa chính kép
                b.HasKey(pp => new { pp.ProductID, pp.PromotionID });

                // FK: ProductPromotion → Product
                b.HasOne(pp => pp.Product)
                    .WithMany(p => p.ProductPromotions)
                    .HasForeignKey(pp => pp.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);

                // FK: ProductPromotion → Promotion
                b.HasOne(pp => pp.Promotion)
                    .WithMany(pr => pr.ProductPromotions)
                    .HasForeignKey(pp => pp.PromotionID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
