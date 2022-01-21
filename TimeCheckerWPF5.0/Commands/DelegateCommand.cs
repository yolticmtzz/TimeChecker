using System;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Commands
{

    
    /// Summary:
    ///     A general Delegation Command that offers the CommandBase implementation to any command
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
        ///     overrides the CanExecute method. Check if the command is executable
        ///     based on its predicated result given by the calling command.
        ///         
        /// Parameters:
        ///    execute:
        ///     The implementation of the execute logic
        public override bool CanExecute(object parameter)
        { 
            if (canExecute != null)
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
