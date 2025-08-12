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
    public class InventoryTransaction : BaseEntity<int>
    {
        public int VariantID { get; private set; }
        public string TransactionType { get; private set; }
        public int QuantityChange { get; private set; }
        public int NewStockLevel { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public int? OrderDetailID { get; private set; }
        public int? PurchaseDetailID { get; private set; }

        public ProductVariant Variant { get; private set; } = null!;

        private InventoryTransaction() { }

        public static InventoryTransaction Create(int variantId, string type, int quantityChange, int newStock)
        {
            if (quantityChange == 0)
                throw new ArgumentException("Quantity change cannot be zero.");

            return new InventoryTransaction
            {
                VariantID = variantId,
                TransactionType = type,
                QuantityChange = quantityChange,
                NewStockLevel = newStock,
                TransactionDate = DateTime.UtcNow
            };
        }
    }
}