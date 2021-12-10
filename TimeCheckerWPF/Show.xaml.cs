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

using System.Data;
using System.Data.SqlClient;
using TimeCheckerWPF.Classes;

namespace TimeCheckerWPF
{
    /// <summary>
    /// Interaction logic for Show.xaml
    /// </summary>
    public partial class Show : Page
    {
        public Show()
        {
            InitializeComponent();
        }

        private void MyDataGridd_Loaded(object sender, RoutedEventArgs e)
        {
            

            Connections.sql = "SELECT * FROM Timeentry"; // Command to load data from Database
            Connections.cmd.CommandType = CommandType.Text;
            Connections.cmd.CommandText = Connections.sql;

            Connections.sql_adapt = new SqlDataAdapter(Connections.cmd);
            Connections.tblData = new DataTable();


            Connections.sql_adapt.Fill(Connections.tblData);

            Connections.openConnection();

            MyDataGrid.ItemsSource = Connections.tblData.DefaultView; // return table data into DataGrid

            Connections.closeConnection();
        }
    }
}
