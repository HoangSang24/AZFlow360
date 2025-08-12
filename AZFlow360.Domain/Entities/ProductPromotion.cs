using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class ProductPromotion
    {
        public int ProductID { get; private set; }
        public int PromotionID { get; private set; }

        public Product Product { get; private set; } = null!;
        public Promotion Promotion { get; private set; } = null!;

        private ProductPromotion() { }

        public ProductPromotion(int productId, int promotionId)
        {
            ProductID = productId;
            PromotionID = promotionId;
        }
    }
}
