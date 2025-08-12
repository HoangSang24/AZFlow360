using AZFlow360.Domain.Interfaces;
using AZFlow360.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICategoryRepository Categories { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IInventoryTransactionRepository InventoryTransactions { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public IPromotionRepository Promotions { get; private set; }
        public IPurchaseRepository Purchases { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ISupplierRepository Suppliers { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Customers = new CustomerRepository(_context);
            InventoryTransactions = new InventoryTransactionRepository(_context);
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
            Promotions = new PromotionRepository(_context);
            Purchases = new PurchaseRepository(_context);
            Roles = new RoleRepository(_context);
            Suppliers = new SupplierRepository(_context);
            Users = new UserRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}