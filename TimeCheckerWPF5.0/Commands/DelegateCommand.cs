using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{
    public class DelegateCommand : CommandBase
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public override bool CanExecute(object parameter)
        { 
            if (canExecute == null)
            {
                canExecute?.Invoke(parameter);

            }
            return true;
        }

        public override void Execute(object parameter)
        {
           execute?.Invoke(parameter);
        }

    }

}
