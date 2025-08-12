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
    public class Category : BaseEntity<int>
    {
        public string CategoryName { get; private set; }
        public string? Description { get; private set; }
        public int? ParentCategoryID { get; private set; }

        public Category? ParentCategory { get; private set; }
        private readonly List<Category> _children = new();
        public IReadOnlyCollection<Category> Children => _children.AsReadOnly();

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private Category() { } // EF Core constructor

        public static Category Create(string name, string? description = null, int? parentId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.", nameof(name));

            return new Category
            {
                CategoryName = name,
                Description = description,
                ParentCategoryID = parentId
            };
        }
    }
}