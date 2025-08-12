using AZFlow360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Interfaces
{
    /// <summary>
    /// Giao diện cho kho chứa của thực thể User.
    /// </summary>
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(object id);
        Task<IReadOnlyList<User>> GetAllAsync();
        Task AddAsync(User entity);
        void Update(User entity);
        void Delete(User entity);

        /// <summary>
        /// Tìm kiếm người dùng bằng tên đăng nhập một cách bất đồng bộ.
        /// </summary>
        /// <param name="username">Tên đăng nhập cần tìm.</param>
        /// <returns>Một đối tượng Task chứa người dùng (User) hoặc null nếu không tồn tại.</returns>
        Task<User?> GetByUsernameAsync(string username);
    }
}
