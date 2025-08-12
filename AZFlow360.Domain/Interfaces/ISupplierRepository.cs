using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Supplier.
    /// </summary>
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(object id);
        Task<IReadOnlyList<Supplier>> GetAllAsync();
        Task AddAsync(Supplier entity);
        void Update(Supplier entity);
        void Delete(Supplier entity);
    }
}

