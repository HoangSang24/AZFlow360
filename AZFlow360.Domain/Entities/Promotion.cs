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
    public class Promotion : BaseEntity<int>
    {
        public string PromotionName { get; private set; }
        public string? Description { get; private set; }
        public string TypeCode { get; private set; }
        public decimal DiscountValue { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<ProductPromotion> _productPromotions = new();
        public IReadOnlyCollection<ProductPromotion> ProductPromotions => _productPromotions.AsReadOnly();

        private Promotion() { }

        public static Promotion Create(string name, string typeCode, decimal discountValue, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate) throw new ArgumentException("End date cannot be earlier than start date.");

            return new Promotion
            {
                PromotionName = name,
                TypeCode = typeCode,
                DiscountValue = discountValue,
                StartDate = startDate,
                EndDate = endDate,
                IsActive = true
            };
        }
    }
}