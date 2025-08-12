using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Đại diện cho mẫu Unit of Work, cung cấp quyền truy cập vào tất cả các repository
    /// và quản lý các giao dịch cơ sở dữ liệu.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        IInventoryTransactionRepository InventoryTransactions { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IPromotionRepository Promotions { get; }
        IPurchaseRepository Purchases { get; }
        IRoleRepository Roles { get; }
        ISupplierRepository Suppliers { get; }
        IUserRepository Users { get; }

        /// <summary>
        /// Lưu tất cả các thay đổi đã được thực hiện trong ngữ cảnh (context) vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="cancellationToken">Token để hủy tác vụ.</param>
        /// <returns>Số lượng đối tượng đã được ghi vào cơ sở dữ liệu.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}