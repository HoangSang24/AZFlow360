using AZFlow360.Domain.Entities;
using AZFlow360.Domain.Interfaces;
using AZFlow360.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure.Repositories
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
