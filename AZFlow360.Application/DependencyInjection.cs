using AZFlow360.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation; // <--- THÊM DÒNG NÀY
using AutoMapper; // <--- THÊM DÒNG NÀY

namespace AZFlow360.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Đăng ký tất cả các Validators trong Assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Đăng ký MediatR và các Handler
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // Thêm pipeline behavior để tự động validate
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}