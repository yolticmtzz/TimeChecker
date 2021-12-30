using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.ViewModels
{
    class CheckOutDialogViewModel
    {

        private string dialogComment;

        public string DialogComment {
            get => dialogComment;           
                
            set
            {
                if (value != dialogComment)
                {
                    dialogComment = value;
                }
            }     
          }


        public ICommand CheckOutCommand { get; set; }

        public ICommand CancelCommand { get; set; }


    }
}
