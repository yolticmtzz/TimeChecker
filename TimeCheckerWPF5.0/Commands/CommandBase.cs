using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{

    
    /// Summary:
    ///     A base class for all commands to implement ICommand's Execute, CanExecute and OnCanExcuteChanged
    ///      which are the basic methods, generally a command should be able to "do"
    public abstract class CommandBase : ICommand
    {


        /// Summary:
        ///     Occurs when changes occur that affect whether or not the command should execute.
        public event EventHandler CanExecuteChanged;


        /// Summary:
        ///     Defines the method that determines whether the command can execute in its current
        ///     state.
        ///
        /// Parameters:
        ///   parameter:
        ///     Data used by the command. If the command does not require data to be passed,
        ///
        /// Returns:
        ///     true if this command can be executed; otherwise, false.
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// Summary:
        ///     Defines the method to be called when the command is invoked.
        ///
        /// Parameters:
        ///   parameter:
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        public abstract void Execute(object parameter);


        /// Summary:
        ///     Invokes the CanExecuteChanged EvenHandler with the command and its arguments to reconsider the CanExcute
        ///     method
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
