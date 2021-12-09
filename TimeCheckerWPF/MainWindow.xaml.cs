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

            Connection.sql = "use TimeChecker select * from Timeentry"; // load data from Database
            Connection.cmd.CommandType = CommandType.Text;
            Connection.cmd.CommandText = Connection.sql;

            Connection.sql_adapt = new SqlDataAdapter(Connection.cmd);

            Connection.tblData = new DataTable();
            Connection.sql_adapt.Fill(Connection.tblData);

            MyDataGrid.ItemsSource = Connection.tblData.DefaultView; // return table data into DataGrid
            
            Connection.closeConnection();

        }
    }
}


//DbClass.sql = "use TimeChecker SELECT [ID], [Type], [Date], [Time], [user], [User] FROM Timeentry;";


