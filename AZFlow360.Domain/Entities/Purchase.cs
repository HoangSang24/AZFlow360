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
    public class Purchase : BaseEntity<int>
    {
        public string PurchaseCode { get; private set; }
        public int SupplierID { get; private set; }
        public int UserID { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; }

        public Supplier Supplier { get; private set; } = null!;
        public User User { get; private set; } = null!;

        private readonly List<PurchaseDetail> _purchaseDetails = new();
        public IReadOnlyCollection<PurchaseDetail> PurchaseDetails => _purchaseDetails.AsReadOnly();

        private Purchase() { }

        public static Purchase Create(string code, int supplierId, int userId)
        {
            return new Purchase
            {
                PurchaseCode = code,
                SupplierID = supplierId,
                UserID = userId,
                PurchaseDate = DateTime.UtcNow,
                Status = "Received",
                TotalAmount = 0
            };
        }
    }
}