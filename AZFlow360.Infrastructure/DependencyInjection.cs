using AZFlow360.Domain.Interfaces;
using AZFlow360.Infrastructure.Persistence;
using AZFlow360.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Đăng ký AppDbContext
            // Lấy chuỗi kết nối từ file appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Cấu hình DbContext để sử dụng SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString,
                    // Đảm bảo Entity Framework biết nơi tìm các file migrations
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // 2. Đăng ký UnitOfWork và các Repositories
            // Sử dụng AddScoped để đảm bảo UnitOfWork được tạo mới cho mỗi request HTTP,
            // nhưng được tái sử dụng trong cùng một request đó.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Không cần đăng ký từng repository riêng lẻ (ví dụ: IUserRepository)
            // vì UnitOfWork đã chịu trách nhiệm khởi tạo chúng.

            return services;
        }
    }
}