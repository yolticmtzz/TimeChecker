using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{


    /// <summary>
    ///     A base class for all commands to implement ICommand's Execute, CanExecute and OnCanExcuteChanged
    ///     which are the basic methods, generally a command should be able to "do"
    /// </summary>
    public abstract class CommandBase : ICommand
    {

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        ///     A event to be registered to
        /// </summary>
        public virtual event EventHandler CanExecuteChanged;
            

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current
        ///     state.
        ///
        /// <paramref name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </paramref>
        ///        
        /// <return>
        ///     true if this command can be executed; otherwise, false.
        /// </return>
        ///
        /// </summary>    
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        ///
        /// <paramref name="parameter">
        ///     Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null.
        ///  </paramref>
        ///  
        ///     </summary>
        public abstract void Execute(object parameter);


        /// <summary>
        ///     Invokes the CanExecuteChanged EvenHandler with the command and its arguments to reconsider the CanExcute
        ///     method
        /// </summary>
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
