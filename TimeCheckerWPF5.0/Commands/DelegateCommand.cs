using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{

    
    /// Summary:
    ///     A general Delegation Command that implements the CommandBase implementation
    ///     This way each command can implement its own Execute and CanExecute logic     
    public class DelegateCommand : CommandBase
    {

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        /// Summary:
        /// Initializes a new instance of a Delegate Command asking for the canExecute logic and the
        /// execute logic of the command plans to execute
        ///         
        /// Parameters:
        ///   canExecute:
        ///     The criteria the canExecute must match to execute
        ///    execute:
        ///     The implementation of the execute logic
        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        /// Summary:
        /// Initializes a new instance of a Delegate Command asking just for the execute logic
        /// of the command plans to execute. An instance without checking, if its executable
        ///         
        /// Parameters:
        ///    execute:
        ///     The implementation of the execute logic
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        /// Summary:
        ///     Overrides the CanExecute method. Check if the command is executable
        ///     based on its predicated result given by the calling command. If there 
        ///     wasn't any, it executes anyways.
        ///         
        /// Parameters:
        ///    parameter:
        ///     The predicate that will return its result based on the criterias implemented in the calling command
        ///     and thus determine if its executable or not
        public override bool CanExecute(object parameter)
        { 
            if (canExecute != null)
            {
                canExecute?.Invoke(parameter);
            }
            return true;
        }

        /// Summary:
        ///     Overrides the Execute method with the implemented logic from the calling command.
        ///         
        /// Parameters:
        ///    parameter:
        ///     The exectution logic implemented by the calling command.
        public override void Execute(object parameter)
        {
           execute?.Invoke(parameter);
        }

    }

}
