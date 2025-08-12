using AZFlow360.Application.Common.Interfaces;
using AZFlow360.Infrastructure.Persistence;
using AZFlow360.WPFClient.Models;
using AZFlow360.WPFClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AZFlow360.WPFClient.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        // Các trường private để lưu trữ dịch vụ được tiêm vào
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IServiceProvider _serviceProvider; // Thêm trường n

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; } // Thêm command này
        // ... các command khác

        // Sửa constructor để nhận các dịch vụ
        public LoginViewModel(AppDbContext context, IPasswordHasher passwordHasher, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            LoginCommand = new RelayCommand<PasswordBox>(ExecuteLogin, CanExecuteLogin);
            NavigateToSignUpCommand = new RelayCommand(ExecuteNavigateToSignUp); // Khởi tạo command
        }

        private bool CanExecuteLogin(PasswordBox passwordBox)
        {
            return !string.IsNullOrEmpty(Username) && passwordBox != null && !string.IsNullOrEmpty(passwordBox.Password);
        }

        private void ExecuteLogin(PasswordBox passwordBox)
        {
            ErrorMessage = "";
            var password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Username.Equals(Username));

            if (user != null)
            {
                // Dùng dịch vụ IPasswordHasher đã được tiêm vào để xác thực mật khẩu
                if (_passwordHasher.Verify(password, user.PasswordHash))
                {
                    // Đăng nhập thành công
                    MessageBox.Show($"Chào mừng {user.Username} đã quay trở lại!");

                    // Đóng cửa sổ đăng nhập
                    System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
                }
                else
                {
                    ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            else
            {
                ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }
        }

        private void ExecuteNavigateToSignUp()
        {
            // Lấy SignUpWindow và SignUpViewModel từ DI container
            var signUpWindow = _serviceProvider.GetRequiredService<SignUpWindow>();
            signUpWindow.DataContext = _serviceProvider.GetRequiredService<SignUpViewModel>();
            signUpWindow.Show();

            // Đóng cửa sổ đăng nhập hiện tại
            System.Windows.Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
        }

        // ... phần còn lại của class
    }
}
