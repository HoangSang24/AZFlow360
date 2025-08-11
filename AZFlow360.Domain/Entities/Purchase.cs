using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseID { get; set; }

        [Required, MaxLength(50)]
        public string PurchaseCode { get; set; } = null!;

        [ForeignKey(nameof(Supplier))]
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(50)]
        public string Status { get; set; } = "Received";

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0m;

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}
