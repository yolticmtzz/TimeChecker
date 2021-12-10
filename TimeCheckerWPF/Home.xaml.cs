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
using System.Threading;

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
            ConnectionSQL.openConnection();

            ConnectionSQL.sql = "INSERT INTO Timeentry VALUES (@Type, @Date, @Time, @Comment, @User)"; // Command to insert data to Database
            ConnectionSQL.cmd.CommandType = CommandType.Text;
            ConnectionSQL.cmd.CommandText = ConnectionSQL.sql;
            var bl = new BusinessLogic();


            // Add Values form the TextBox to DataBase
            ConnectionSQL.cmd.Parameters.AddWithValue("@Type", '2');
            //ConnectionSQL.cmd.Parameters.AddWithValue("@Type", TextBoxType.Text);
            ConnectionSQL.cmd.Parameters.AddWithValue("@Date", bl.Date());
            //ConnectionSQL.cmd.Parameters.AddWithValue("@Date", TextBoxDate.Text);
            ConnectionSQL.cmd.Parameters.AddWithValue("@Time", bl.Time());
            //Connections.cmd.Parameters.AddWithValue("@Time", TextBoxTime.Text);
            ConnectionSQL.cmd.Parameters.AddWithValue("@Comment", TextBoxComment.Text);
            //ConnectionSQL.cmd.Parameters.AddWithValue("@Comment", TextBoxComment.Text);
            ConnectionSQL.cmd.Parameters.AddWithValue("@User", "Jose Panov");
           // ConnectionSQL.cmd.Parameters.AddWithValue("@User", TextBoxUser.Text);


            ConnectionSQL.cmd.ExecuteNonQuery();

            ConnectionSQL.cmd.Parameters.Clear();

            ConnectionSQL.closeConnection();

            clearData();

            
        }


    }
}


//var timer = new Timer(this.CallbackFunction, "Es ist {0}:{1} Uhr", 0, 1000);