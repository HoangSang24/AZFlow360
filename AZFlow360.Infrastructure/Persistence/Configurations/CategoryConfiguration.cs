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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("CategoryID").ValueGeneratedOnAdd();

            builder.HasIndex(x => x.CategoryName).IsUnique();
            builder.Property(x => x.CategoryName).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.ParentCategory)
                   .WithMany(x => x.Children)
                   .HasForeignKey(x => x.ParentCategoryID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}