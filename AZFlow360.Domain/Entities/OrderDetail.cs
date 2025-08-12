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
    public class OrderDetail : BaseEntity<int>
    {
        public int OrderID { get; private set; }
        public int VariantID { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal => Quantity * UnitPrice;

        public Order Order { get; private set; } = null!;
        public ProductVariant Variant { get; private set; } = null!;

        private OrderDetail() { }

        internal OrderDetail(int orderId, int variantId, int quantity, decimal unitPrice)
        {
            OrderID = orderId;
            VariantID = variantId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void AddQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity to add must be positive.");
            Quantity += quantity;
        }
    }
}
