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
    public class Customer : BaseEntity<int>
    {
        public string FullName { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public string? Address { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customer() { }

        public static Customer Create(string fullName, string? phone = null, string? email = null, string? address = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Customer full name is required", nameof(fullName));

            return new Customer
            {
                FullName = fullName,
                Phone = phone,
                Email = email,
                Address = address,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}