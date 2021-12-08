using System;
using System.Collections.Generic;
using System.Data;
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


        public MainWindow()
        {
            InitializeComponent();
        }


        private void CheckInOut_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                connection.Open();
                SqlCommand command = new SqlCommand("use TimeChecker insert into Timeentry([Comment]) Values('fff')", connection);
                SqlCommand command2 = new SqlCommand("use TimeChecker insert into Timeentry([User]) Values('Panov')", connection);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception)
            {
                throw;
            }

        }

 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand("use TimeChecker select * from Timeentry", connection);
            //    SqlDataReader writer = null;
            //    writer = command.ExecuteReader();

            //    string s = "Hallo";


            //    TextBox_Send.Text = s;

            //    connection.Close();

            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            //Timestamp();
        }



        //private void liste_laden()
        //{

            //SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //connection.Open();

            //string sql_Text = "use TimeChecker SELECT * FROM Timeentry";
            //SqlDataAdapter sql_adapt = new SqlDataAdapter(sql_Text, connection);

            //DataTable tblData = new DataTable();
            //sql_adapt.Fill(tblData);

            ////Anzeigen

            //TextBox_Read.DataContext = tblData;

            //TextBox_Read.Text = "Comment";
            //TextBox_Read.Text = "[ID]";

        //}


        //private void Timestamp()
        //{

        //    string sNeuer_Wert = TextBox_Send.Text;

        //    string sql_Text = "insert into Timeentry([Comment]) Values('Hallo')";

        //    db_execute(sql_Text);

        //}


        //private int db_execute(string sql_Text2)
        //{
            
        //    SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    connection.Open();
        //    SqlCommand command = new SqlCommand("sql_Text2", connection);
            
        //    int nResult = command.ExecuteNonQuery();

        //    return nResult;

        //}


    }
    
}
