using System;
using System.Windows.Input;
using TimeCheckerWPF5._0.Commands;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class CheckOutDialogViewModel : ViewModelBase
    {
        public event EventHandler CheckOut;
        public event EventHandler Cancel;


        public CheckOutDialogViewModel()
        {
            CheckOutCommand = new DelegateCommand((o) => CheckOut?.Invoke(this, EventArgs.Empty));
            CancelCommand = new DelegateCommand((o) => Cancel?.Invoke(this, EventArgs.Empty));
        }

        public static string DialogText
        {
            get => "Before definitely \"Check Out\", you have the possibility to leave a comment or check out without. \n \n(Your Timewatch will be reset after checking out. \nClick \"Cancel\" if you want to remain checked-in)";
        }

        private string dialogComment = "";

        public string DialogComment {
            get => dialogComment;           
                
            set
            {
                   dialogComment = value;
            }     
          }


        public ICommand CheckOutCommand { get; set; }

        public ICommand CancelCommand { get; set; }


    }
}
