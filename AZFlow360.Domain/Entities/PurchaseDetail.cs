using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class PurchaseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseDetailID { get; set; }

        [ForeignKey(nameof(Purchase))]
        public int PurchaseID { get; set; }
        public Purchase Purchase { get; set; } = null!;

        [ForeignKey(nameof(Variant))]
        public int VariantID { get; set; }
        public ProductVariant Variant { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitCost { get; set; }
    }
}
