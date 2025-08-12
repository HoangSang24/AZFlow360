using AZFlow360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string ProductName { get; private set; }
        public string? Description { get; private set; }
        public int CategoryID { get; private set; }
        public int? SupplierID { get; private set; }

        public Category Category { get; private set; } = null!;
        public Supplier? Supplier { get; private set; }

        private readonly List<ProductVariant> _variants = new();
        public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

        private readonly List<ProductPromotion> _productPromotions = new();
        public IReadOnlyCollection<ProductPromotion> ProductPromotions => _productPromotions.AsReadOnly();

        private Product() { }

        public static Product Create(string name, int categoryId, string? description = null, int? supplierId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            return new Product
            {
                ProductName = name,
                CategoryID = categoryId,
                Description = description,
                SupplierID = supplierId
            };
        }
    }
}