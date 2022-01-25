using System.Windows;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Injects the MainView and sets it as DataContext
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainView 
        /// and sets the injected MainViewModel from StartUp as DataContext
        /// Also registers an ExitEventHandler as ExitApp method
        /// to the exit current application logic.
        /// 
        /// <paramref name="viewModel">
        ///     injection of a MainViewModel from the StartUp method in
        ///     the app xaml (DI).
        /// />
        /// </summary>
        public MainWindow(MainViewModel viewModel)
        {
            
            DataContext = viewModel;
            InitializeComponent();

            Application.Current.Exit += new ExitEventHandler(ExitApp);
        }

        /// <summary>
        /// Implements a MessageBox Pop uf, when the Application is terminated  
        ///
        /// <paramref name="sender">
        ///     the object with what the application is closed (f.e. close-button)
        /// </paramref>
        /// 
        /// <paramref name="e">
        ///     the ExitEventArgs, irrelevant in this implementation
        /// </paramref>
        /// 
        /// </summary>
        private void ExitApp(object sender, ExitEventArgs e)
        {
            MessageBox.Show("TimeChecker was terminated");
        }
    }
}

