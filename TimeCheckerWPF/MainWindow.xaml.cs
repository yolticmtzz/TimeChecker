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

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            //Connection.openConnection();

            //TextBox.Text = Connection.ReadToTextBox();
            //ListBox.ItemsSource = Connection.ReadToListBox();

            //Connection.closeConnection();
        }

        private void BTextBox_Click(object sender, RoutedEventArgs e)
        {

            Connection.openConnection();

            TextBox.Text = Connection.ReadToTextBox();

            Connection.closeConnection();

        }

        private void BListBox_Click(object sender, RoutedEventArgs e)
        {
            Connection.openConnection();

            ListBox.ItemsSource = Connection.ReadToListBox();

            Connection.closeConnection();

        }
    }
}


//DbClass.sql = "use TimeChecker SELECT [ID], [Type], [Date], [Time], [user], [User] FROM Timeentry;";
//DbClass.cmd.CommandType = CommandType.Text;
//DbClass.cmd.CommandText = DbClass.sql;

//DbClass.da = new SqlDataAdapter(DbClass.cmd);
//DbClass.dt = new DataTable();
//DbClass.da.Fill(DbClass.dt);

//ListBox.ItemsSource = DbClass.dt.DefaultView;
//TextBox.Text = DbClass.sql;


