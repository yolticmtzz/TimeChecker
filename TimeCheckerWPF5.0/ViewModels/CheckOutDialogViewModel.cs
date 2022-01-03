using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeCheckerWPF5._0.Models;

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
