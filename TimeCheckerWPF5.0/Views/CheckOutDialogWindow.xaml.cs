using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
