using System;
using System.Windows.Input;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LogInDialogViewModel : ViewModelBase
    {
        public Employee User { get; set; }

        public event EventHandler Start;
        public event EventHandler Cancel;

        private string firstname;
        public string Firstname
        {
            get => firstname;

            set
            {
                firstname = value;
            }
        }
        private string lastname;
        public string Lastname
        {
            get => lastname;

            set
            {
                lastname = value;
            }
        }

        public LogInDialogViewModel()
        {
            StartCommand = new DelegateCommand((o) => Start?.Invoke(this, EventArgs.Empty));
            CancelCommand = new DelegateCommand((o) => Cancel?.Invoke(this, EventArgs.Empty));
            User = new Employee(Firstname, Lastname);
        }

        public ICommand StartCommand { get; set; }

        public ICommand CancelCommand { get; set; }

    }
}