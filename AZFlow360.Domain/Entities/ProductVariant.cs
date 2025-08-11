using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class ProductVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariantID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;

        [Required, MaxLength(100)]
        public string SKU { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; } = 0m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        // Stock: integer, track inventory
        public int Stock { get; set; } = 0;

        [MaxLength(500)]
        public string? ImageURL { get; set; }

        public bool IsActive { get; set; } = true;

        // Concurrency token (optimistic concurrency)
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
    }
}
