using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class InventoryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionID { get; set; }

        [ForeignKey(nameof(Variant))]
        public int VariantID { get; set; }
        public ProductVariant Variant { get; set; } = null!;

        [Required, MaxLength(50)]
        public string TransactionType { get; set; } = null!; // Sale, Purchase, Return, Adjustment

        public int QuantityChange { get; set; }

        [MaxLength(50)]
        public string? ReferenceID { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
