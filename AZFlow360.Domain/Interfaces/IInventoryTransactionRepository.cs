using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể InventoryTransaction.
    /// </summary>
    public interface IInventoryTransactionRepository
    {
        Task<InventoryTransaction?> GetByIdAsync(object id);
        Task<IReadOnlyList<InventoryTransaction>> GetAllAsync();
        Task AddAsync(InventoryTransaction entity);
        void Update(InventoryTransaction entity);
        void Delete(InventoryTransaction entity);
    }
}
