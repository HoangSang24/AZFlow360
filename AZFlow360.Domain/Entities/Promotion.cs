using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }

        [Required, MaxLength(255)]
        public string PromotionName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string TypeCode { get; set; } = null!; // 'PERCENT' / 'FIXED_AMOUNT'

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Quan hệ N-N với Product
        public ICollection<ProductPromotion> ProductPromotions { get; set; }
    }
}
