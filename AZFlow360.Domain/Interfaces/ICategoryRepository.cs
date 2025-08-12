using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa (repository) của thực thể Category.
    /// </summary>
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(object id);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task AddAsync(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
