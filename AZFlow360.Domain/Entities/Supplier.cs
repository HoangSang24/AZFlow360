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
    public class Supplier : BaseEntity<int>
    {
        public string SupplierName { get; private set; }
        public string? ContactName { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public string? Address { get; private set; }

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private readonly List<Purchase> _purchases = new();
        public IReadOnlyCollection<Purchase> Purchases => _purchases.AsReadOnly();

        private Supplier() { }

        public static Supplier Create(string name, string? contactName = null, string? phone = null, string? email = null, string? address = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Supplier name is required", nameof(name));

            return new Supplier
            {
                SupplierName = name,
                ContactName = contactName,
                Phone = phone,
                Email = email,
                Address = address
            };
        }
    }
}