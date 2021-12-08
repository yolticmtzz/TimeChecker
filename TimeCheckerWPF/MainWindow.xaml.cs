using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TimeCheckerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckInOut1_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("use TimeChecker select * from Timeentry", connection);
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                string s = "";

                while (reader.Read())
                {

                    s = s + reader["User"].ToString();
                }

                TextBox_Read.Text = s;

                connection.Close();

            }
            catch (Exception)
            {
                throw;           
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("use TimeChecker select * from Timeentry", connection);
                SqlDataReader writer = null;
                writer = command.ExecuteReader();

                string s = "Hallo";


                TextBox_Send.Text = s;

                connection.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
