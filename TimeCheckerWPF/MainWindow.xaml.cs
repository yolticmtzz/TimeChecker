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

//
using System.Data;
using System.Data.SqlClient;
using TimeCheckerWPF.Classes;


namespace TimeCheckerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
        }


        private void Button_Show(object sender, RoutedEventArgs e)
        {
            Main.Content = new Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
        }
    }
}


//DbClass.sql = "use TimeChecker SELECT [ID], [Type], [Date], [Time], [user], [User] FROM Timeentry;";


