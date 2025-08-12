using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Customer.
    /// </summary>
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(object id);
        Task<IReadOnlyList<Customer>> GetAllAsync();
        Task AddAsync(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
