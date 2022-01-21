using System.Windows;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Views
{
    /// <summary>
    /// Interaction logic for CheckOutDialogView.xaml
    /// </summary>
    public partial class CheckOutDialogWindow : Window
    {
        public CheckOutDialogWindow()
        {
            InitializeComponent();
            var vm = DataContext as CheckOutDialogViewModel;
            vm.CheckOut += (o, e) => DialogResult = true;
            vm.Cancel += (o, e) => DialogResult = false;
        }
    }
}
