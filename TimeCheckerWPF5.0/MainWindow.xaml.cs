using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;
using TimeCheckerWPF5._0.Views;

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NavigationStore _navigationStore;

        public MainWindow(MainViewModel viewModel, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            DataContext = viewModel;
            InitializeComponent();

            Application.Current.Exit += new ExitEventHandler(ExitApp);
        }


        private void ExitApp(object sender, ExitEventArgs e)
        {
            MessageBox.Show("TimeChecker was terminated");
        }
    }
}

