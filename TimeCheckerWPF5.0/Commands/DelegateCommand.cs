using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{


    /// <summary>
    ///     A general Delegation Command that implements the CommandBase implementation
    ///     This way each command can implement its own Execute and CanExecute logic   
    /// </summary>
    public class DelegateCommand : CommandBase
    {

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        /// <summary>
        /// Initializes a new instance of a Delegate Command asking for the canExecute logic and the
        /// execute logic of the command plans to execute
        ///         
        /// <paramref name="canExecute">
        /// The criteria the canExecute must match to execute
        /// </paramref>
        /// <paramref name="execute">
        /// The implementation of the execute logic
        /// </paramref>
        ///
        /// </summary>
        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of a Delegate Command asking just for the execute logic
        /// of the command plans to execute. An instance without checking, if its executable
        ///         
        /// <paramref name="execute">:
        ///    The implementation of the execute logic
        ///    </paramref>
        ///    
        /// </summary>
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        ///     Overrides the CanExecute method. Check if the command is executable
        ///     based on its predicated result given by the calling command. If there 
        ///     wasn't any, it executes anyways.
        ///         
        /// <paramref name="parameter">:
        ///     The predicate that will return its result based on the criterias implemented in the calling command
        ///     and thus determine if its executable or not
        /// </paramref>
        /// 
        /// </summary>
        public override bool CanExecute(object parameter)
        { 
            if (canExecute != null)
            {
                canExecute?.Invoke(parameter);
            }
            return true;
        }

        /// <summary>
        ///     Overrides the Execute method with the implemented logic from the calling command.
        ///         
        /// <paramref name="parameter">
        ///    The exectution logic implemented by the calling command.
        /// </paramref>
        ///    
        /// </summary>
        public override void Execute(object parameter)
        {
           execute?.Invoke(parameter);
        }

    }

}
