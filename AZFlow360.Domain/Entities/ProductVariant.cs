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
    public class ProductVariant : BaseEntity<int>
    {
        public int ProductID { get; private set; }
        public string SKU { get; private set; }
        public decimal CostPrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public int Stock { get; private set; }
        public byte[]? RowVersion { get; private set; }

        public Product Product { get; private set; } = null!;

        private ProductVariant() { }

        internal ProductVariant(int productId, string sku, decimal salePrice, decimal costPrice, int stock)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU is required.", nameof(sku));
            if (salePrice < 0) throw new ArgumentException("Sale price cannot be negative.", nameof(salePrice));

            ProductID = productId;
            SKU = sku;
            SalePrice = salePrice;
            CostPrice = costPrice;
            Stock = stock;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            Stock += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            if (Stock < quantity) throw new InvalidOperationException("Not enough stock available.");
            Stock -= quantity;
        }
    }
}