using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Role.
    /// </summary>
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(object id);
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task AddAsync(Role entity);
        void Update(Role entity);
        void Delete(Role entity);
    }
}
