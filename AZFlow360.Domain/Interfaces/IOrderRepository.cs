using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Order.
    /// </summary>
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(object id);
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task AddAsync(Order entity);
        void Update(Order entity);
        void Delete(Order entity);
    }
}
