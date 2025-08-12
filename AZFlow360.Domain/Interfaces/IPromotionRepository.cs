using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể Promotion.
    /// </summary>
    public interface IPromotionRepository
    {
        Task<Promotion?> GetByIdAsync(object id);
        Task<IReadOnlyList<Promotion>> GetAllAsync();
        Task AddAsync(Promotion entity);
        void Update(Promotion entity);
        void Delete(Promotion entity);
    }
}
