using System;
using System.Windows.Input;

namespace TinyMVVM
{
    public class RelayCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            return this.canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(T parameter)
        {
            execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T) parameter);
        }

        void ICommand.Execute(object parameter)
        {
            execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null) 
            : base((param)=>execute(), (param)=>canExecute?.Invoke() ?? true)
        {
        }
    }
}
