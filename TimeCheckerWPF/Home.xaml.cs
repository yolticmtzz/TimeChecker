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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        public void clearData()
        {
            TextBoxType.Clear();
            TextBoxDate.Clear();
            TextBoxTime.Clear();
            TextBoxComment.Clear();
            TextBoxUser.Clear();

        }

        private void CheckInOut_Click(object sender, RoutedEventArgs e)
        {


            Connections.sql = "INSERT INTO Timeentry VALUES (@Type, @Date, @Time, @Comment, @User)"; // Command to insert data to Database
            Connections.cmd.CommandType = CommandType.Text;
            Connections.cmd.CommandText = Connections.sql;

            // Add Values form the TextBox to DataBase
            Connections.cmd.Parameters.AddWithValue("@Type", TextBoxType.Text);
            Connections.cmd.Parameters.AddWithValue("@Date", TextBoxDate.Text);
            Connections.cmd.Parameters.AddWithValue("@Time", TextBoxTime.Text);
            Connections.cmd.Parameters.AddWithValue("@Comment", TextBoxComment.Text);
            Connections.cmd.Parameters.AddWithValue("@User", TextBoxUser.Text);


            Connections.openConnection();

            Connections.cmd.ExecuteNonQuery();

            Connections.cmd.Parameters.Clear();

            Connections.closeConnection();

            clearData();

            
        }


    }
}


