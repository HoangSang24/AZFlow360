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
    public class User : BaseEntity<int>
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public int RoleID { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Role Role { get; private set; } = null!;
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
        private readonly List<Purchase> _purchases = new();
        public IReadOnlyCollection<Purchase> Purchases => _purchases.AsReadOnly();

        private User() { }

        public static User Create(string username, string passwordHash, string fullName, int roleId, string? phone, string? email)
        {
            return new User
            {
                Username = username,
                PasswordHash = passwordHash,
                FullName = fullName,
                Phone = phone,
                Email = email,
                RoleID = roleId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateProfile(string fullName, string? phone, string? email)
        {
            FullName = fullName;
            Phone = phone;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}