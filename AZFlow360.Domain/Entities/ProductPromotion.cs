using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Entities
{
    public class ProductPromotion
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int PromotionID { get; set; }
        public Promotion Promotion { get; set; }
    }
}
