using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AZFlow360.WPFClient.Models
{
    /// <summary>
    /// Một lớp Command cơ bản triển khai ICommand để chuyển tiếp việc thực thi đến các delegate.
    /// Phiên bản này hỗ trợ tham số kiểu T.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Khởi tạo một instance mới của RelayCommand.
        /// </summary>
        /// <param name="execute">Delegate để thực thi khi command được gọi.</param>
        /// <param name="canExecute">Delegate để kiểm tra xem command có thể thực thi không.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Sự kiện được kích hoạt khi có thay đổi ảnh hưởng đến việc command có nên thực thi hay không.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Xác định xem command có thể thực thi trong trạng thái hiện tại hay không.
        /// </summary>
        /// <param name="parameter">Dữ liệu được sử dụng bởi command. Nếu command không yêu cầu dữ liệu, có thể đặt là null.</param>
        /// <returns>true nếu command này có thể được thực thi; ngược lại là false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        /// <summary>
        /// Thực thi logic của command.
        /// </summary>
        /// <param name="parameter">Dữ liệu được sử dụng bởi command. Nếu command không yêu cầu dữ liệu, có thể đặt là null.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    // Bạn có thể giữ lại lớp RelayCommand không generic để dùng cho các command không cần tham số.
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();
    }
}
