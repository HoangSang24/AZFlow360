using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Product.
    /// </summary>
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(object id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task AddAsync(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}