using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebitExpress.Mvvm
{
    public sealed class DelegateAsyncCommand : ICommand
    {
        private readonly Func<Task> _executedMethod;
        private readonly Func<bool> _canExecuteMethod;

        public DelegateAsyncCommand(Func<Task> executedMethod, Func<bool> canExecuteMethod = null)
        {
            _executedMethod = executedMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public Task Execute()
        {
            return _executedMethod();
        }

        public bool CanExecute()
        {
            return _canExecuteMethod == null || _canExecuteMethod();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object parameter)
        {
            await Execute();
        }
    }

    public sealed class DelegateAsyncCommand<T> : ICommand
    {
        private readonly Func<T, Task> _executedMethod;
        private readonly Func<T, bool> _canExecuteMethod;

        public DelegateAsyncCommand(Func<T, Task> executedMethod, Func<T, bool> canExecuteMethod = null)
        {
            _executedMethod = executedMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public Task Execute(object parameter = null)
        {
            var param = (T) parameter;
            return _executedMethod(param);
        }

        public bool CanExecute(object parameter = null)
        {
            var param = (T)parameter;
            return _canExecuteMethod == null || _canExecuteMethod(param);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            await Execute(parameter);
        }
    }
}