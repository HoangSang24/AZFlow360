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
    public class Order : BaseEntity<int>
    {
        public string OrderCode { get; private set; }
        public int? CustomerID { get; private set; }
        public int UserID { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; }

        public Customer? Customer { get; private set; }
        public User User { get; private set; } = null!;

        private readonly List<OrderDetail> _orderDetails = new();
        public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

        private Order() { }

        public static Order Create(string orderCode, int userId, int? customerId)
        {
            return new Order
            {
                OrderCode = orderCode,
                UserID = userId,
                CustomerID = customerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending", // Initial status
                TotalAmount = 0
            };
        }

        public void AddDetail(int variantId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");

            var existingDetail = _orderDetails.FirstOrDefault(d => d.VariantID == variantId);
            if (existingDetail != null)
            {
                existingDetail.AddQuantity(quantity);
            }
            else
            {
                var newDetail = new OrderDetail(this.Id, variantId, quantity, unitPrice);
                _orderDetails.Add(newDetail);
            }
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = _orderDetails.Sum(d => d.Subtotal);
        }
    }
}
