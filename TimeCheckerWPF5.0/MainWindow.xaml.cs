using System.Windows;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

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

