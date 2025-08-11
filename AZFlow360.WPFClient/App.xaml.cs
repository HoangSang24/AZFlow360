using AZFlow360.Infrastructure;
using AZFlow360.Infrastructure.Persistence;
using AZFlow360.WPFClient.ViewModels;
using AZFlow360.WPFClient.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace AZFlow360.WPFClient
{
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;

        public App()
        {
            // Thiết lập host để xây dựng service provider (bộ chứa DI)
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // Cấu hình để đọc file appsettings.json
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services, context.Configuration);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Gọi phương thức đăng ký từ project Infrastructure
            services.AddInfrastructure(configuration);

            // Đăng ký các ViewModel
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SignUpViewModel>(); // <-- Thêm dòng này

            // Đăng ký các Window
            services.AddTransient<LoginWindow>();
            services.AddTransient<SignUpWindow>(); // <-- Thêm dòng này
                                                   // services.AddTransient<MainWindow>(); // Giả sử bạn có MainWindow

            // Thêm IServiceProvider để có thể inject vào ViewModel
            services.AddSingleton<IServiceProvider>(sp => sp);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // Lấy Window và ViewModel từ bộ chứa DI
            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            loginWindow.DataContext = _host.Services.GetRequiredService<LoginViewModel>();

            loginWindow.Show();

            base.OnStartup(e);
        }
    } 
}
