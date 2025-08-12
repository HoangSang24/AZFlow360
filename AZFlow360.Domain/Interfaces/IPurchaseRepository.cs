using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Purchase.
    /// </summary>
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetByIdAsync(object id);
        Task<IReadOnlyList<Purchase>> GetAllAsync();
        Task AddAsync(Purchase entity);
        void Update(Purchase entity);
        void Delete(Purchase entity);
    }
}
