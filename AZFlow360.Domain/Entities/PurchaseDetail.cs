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
    public class PurchaseDetail : BaseEntity<int>
    {
        public int PurchaseID { get; private set; }
        public int VariantID { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitCost { get; private set; }
        public decimal Subtotal => Quantity * UnitCost;

        public Purchase Purchase { get; private set; } = null!;
        public ProductVariant Variant { get; private set; } = null!;

        private PurchaseDetail() { }

        internal PurchaseDetail(int purchaseId, int variantId, int quantity, decimal unitCost)
        {
            PurchaseID = purchaseId;
            VariantID = variantId;
            Quantity = quantity;
            UnitCost = unitCost;
        }
    }
}