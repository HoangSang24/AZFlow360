using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required, MaxLength(255)]
        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Supplier))]
        public int? SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductPromotion> ProductPromotions { get; set; }
    }
}
