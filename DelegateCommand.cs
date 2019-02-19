using System;
using System.Windows.Input;

namespace DebitExpress.Mvvm
{
    public sealed class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecuteMethod;
        private readonly Action _executeMethod;

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter = null)
        {
            if (_canExecuteMethod != null) return _canExecuteMethod();
            return _executeMethod != null;
        }

        public void Execute(object parameter = null)
        {
            _executeMethod?.Invoke();
        }
    }

    public sealed class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecuteMethod;
        private readonly Action<T> _executeMethod;

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter = null)
        {
            if (_canExecuteMethod == null) return _executeMethod != null;
            var param = (T)parameter;
            return _canExecuteMethod(param);
        }

        public void Execute(object parameter = null)
        {
            _executeMethod?.Invoke((T)parameter);
        }
    }
}