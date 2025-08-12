using AZFlow360.Domain.Entities;
using AZFlow360.Domain.Interfaces;
using AZFlow360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .Include(u => u.Role) // Eager loading Role
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
