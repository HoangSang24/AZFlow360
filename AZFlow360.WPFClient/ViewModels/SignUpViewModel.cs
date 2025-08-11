// File: AZFlow360.WPFClient/ViewModels/SignUpViewModel.cs

using AZFlow360.Application.Common.Interfaces;
using AZFlow360.Domain.Entities;
using AZFlow360.Infrastructure.Persistence;
using AZFlow360.WPFClient.Models;
using AZFlow360.WPFClient.Views;
using Microsoft.EntityFrameworkCore; // Cần thiết cho phương thức .AnyAsync()
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations; // Cần thiết cho [EmailAddress]
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AZFlow360.WPFClient.ViewModels
{
    public class SignUpViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IServiceProvider _serviceProvider;

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            // Gọi SetProperty để tự động thông báo cho UI khi giá trị thay đổi
            set { if (SetProperty(ref _fullName, value)) SignUpCommand.RaiseCanExecuteChanged(); }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set { if (SetProperty(ref _username, value)) SignUpCommand.RaiseCanExecuteChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { if (SetProperty(ref _email, value)) SignUpCommand.RaiseCanExecuteChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        // Thay vì ICommand, dùng trực tiếp RelayCommand<T> để có thể gọi RaiseCanExecuteChanged()
        public RelayCommand<object> SignUpCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public SignUpViewModel(AppDbContext context, IPasswordHasher passwordHasher, IServiceProvider serviceProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _serviceProvider = serviceProvider;

            // Khởi tạo command với 2 delegate: một cho việc thực thi và một để kiểm tra điều kiện thực thi
            SignUpCommand = new RelayCommand<object>(async (param) => await ExecuteSignUp(param), CanExecuteSignUp);
            NavigateToLoginCommand = new RelayCommand(ExecuteNavigateToLogin);
        }

        /// <summary>
        /// Điều kiện để nút Đăng ký có thể được nhấn.
        /// </summary>
        private bool CanExecuteSignUp(object parameter)
        {
            // Nút sẽ được kích hoạt chỉ khi các trường bắt buộc đã được điền
            return !string.IsNullOrWhiteSpace(FullName) &&
                   !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Email);
        }

        /// <summary>
        /// Logic chính khi người dùng nhấn nút Đăng ký.
        /// </summary>
        private async Task ExecuteSignUp(object parameter)
        {
            ErrorMessage = ""; // Xóa thông báo lỗi cũ
            var controls = parameter as object[];
            if (controls == null || controls.Length < 2) return;

            var passwordBox = controls[0] as PasswordBox;
            var confirmPasswordBox = controls[1] as PasswordBox;

            if (passwordBox == null || confirmPasswordBox == null) return;

            var password = passwordBox.Password;
            var confirmPassword = confirmPasswordBox.Password;

            // 1. Kiểm tra đầu vào (bao gồm cả mật khẩu)
            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                return;
            }

            // 2. Kiểm tra định dạng Email
            if (!new EmailAddressAttribute().IsValid(Email))
            {
                ErrorMessage = "Địa chỉ email không hợp lệ.";
                return;
            }

            // 3. Kiểm tra mật khẩu khớp
            if (password != confirmPassword)
            {
                ErrorMessage = "Mật khẩu xác nhận không khớp.";
                return;
            }

            // 4. Kiểm tra sự tồn tại của Username và Email trong DB (bất đồng bộ)
            if (await _context.Users.AnyAsync(u => u.Username == Username))
            {
                ErrorMessage = "Tên đăng nhập này đã tồn tại.";
                return;
            }
            if (await _context.Users.AnyAsync(u => u.Email == Email))
            {
                ErrorMessage = "Email này đã được sử dụng.";
                return;
            }

            // 5. Tạo người dùng mới
            var newUser = new User
            {
                FullName = this.FullName,
                Username = this.Username,
                Email = this.Email,
                PasswordHash = _passwordHasher.Hash(password),
                RoleID = 3, // Mặc định là Salesperson
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // 6. Lưu vào database
            try
            {
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // Chuyển hướng về trang đăng nhập
                ExecuteNavigateToLogin();
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi chi tiết hơn nếu cần (ví dụ: dùng một logging framework)
                ErrorMessage = $"Đã xảy ra lỗi khi tạo tài khoản: {ex.Message}";
            }
        }

        /// <summary>
        /// Chuyển hướng người dùng về cửa sổ Đăng nhập.
        /// </summary>
        private void ExecuteNavigateToLogin()
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.DataContext = _serviceProvider.GetRequiredService<LoginViewModel>();
            loginWindow.Show();

            // Đóng cửa sổ đăng ký hiện tại một cách an toàn
            var currentSignUpWindow = System.Windows.Application.Current.Windows.OfType<SignUpWindow>().FirstOrDefault();
            currentSignUpWindow?.Close();
        }
    }
}