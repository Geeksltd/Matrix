using System;
using System.Windows.Input;

namespace Matrix.Utils
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Predicate<object> CanExecuteFunc { get; set; }
        public Action<object> ExecuteFunc { get; set; }
        public bool CanExecute(object parameter) => CanExecuteFunc(parameter);
        public void Execute(object parameter) => ExecuteFunc(parameter);
        public static Command RegisterCommand(Action<object> method) => new Command() { CanExecuteFunc = obj => true, ExecuteFunc = method };
    }
}