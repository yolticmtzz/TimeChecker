using System;
using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// <summary>
    ///     Represents and handles a pop-up view of type window when the user clicks to check-out.
    ///     Since the check-out window is displayed to the user as an overlay on top of the TimeCheckerView,
    ///     it must be able to intercept and process the user interactions in this pop-up window.
    ///     Event handlers are used for this purpose. 
    ///     The user has three options here
    ///         1. Check out leaving a comment. The comment will be saved in the database later.
    ///         2. Check out without comment (string.empty)
    ///         3. Cancel and stay checked in
    ///      
    ///     The first two options are processed with a OnCheckOutClickEvent,
    ///     the second with a OnCancelClickEvent.
    ///     The events are handled by ButtonClicks (via DelegateCommands).
    ///     The event handlers are registered on the View (CheckOutDialogWindow), 
    ///     because they have to catch the return of the ShowDialog() method of the window.
    ///     See also CodeBehindFile of CheckOutDialogWindow (Views)
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged as well.
    /// 
    ///     </summary>
    public class CheckOutDialogViewModel : ViewModelBase
    {
        public event EventHandler OnCheckOutClickEvent;
        public event EventHandler OnCancelClickEvent;


        /// <summary>
        /// Initializes a new instance of a CheckOutDialogViewModel and initializes 
        /// the CheckOutCommand and the CancelCommand.
        ///     
        /// </summary>
        public CheckOutDialogViewModel()
        {
            CheckOutCommand = new DelegateCommand(ExecuteCheckOutCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        public static string DialogText
        {
            get => "Before definitely \"Check Out\", you have the possibility to leave a comment or check out without." +
                "\n \n(Your Timewatch will be reset after checking out. \nClick \"Cancel\" if you want to remain checked-in)";
        }

        public string DialogComment { get; set; }
            
        public ICommand CheckOutCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     The OnCheckOutClickEvent is invoked
        ///
        /// <paramref name="obj">
        /// the "CheckOut" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private void ExecuteCheckOutCommand(object obj)
        {
            OnCheckOutClickEvent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Delivers the Execute logic for the ICommand Action:
        ///     The OnCancelClickEvent is invoked
        ///
        /// <paramref name="obj">
        /// the "Cancel" button clicked to run the command
        /// </paramref>
        /// 
        /// </summary>
        private void ExecuteCancelCommand(object obj)
        {
            OnCancelClickEvent?.Invoke(this, EventArgs.Empty);
        }

    }
}
