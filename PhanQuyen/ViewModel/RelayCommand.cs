using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        //khi khởi tạo thì truyền điều kiện ủy thác và phương thức ủy thác
        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                return;
            _canExecute = canExecute;
            _execute = execute;
        }



        //điều kiện để chạy command
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }


        //phương thức ủy thác khi gọi command
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        //tạo 1 event có tên tương ứng để ủy thác
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
