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
    public class Role : BaseEntity<int>
    {
        public string RoleName { get; private set; }
        private readonly List<User> _users = new();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        private Role() { }
        public Role(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) throw new ArgumentException("Role name cannot be empty.", nameof(roleName));
            RoleName = roleName;
        }
    }
}
