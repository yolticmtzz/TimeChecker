using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;


namespace TimeCheckerWPF.Classes
{
    class DbClass
    {

        public static string GetConnectionsStrings()
        {
            string strConString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //ConfigurationManager.ConnectionStrings["conString"].ToString();
            return strConString;
        }


        public static string sql;
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand cmd = new SqlCommand("", con);
        public static SqlDataReader rd;
        public static DataTable dt;
        public static SqlDataAdapter da;

        public static void openConnection()
        {

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetConnectionsStrings();
                    con.Open();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The system failed to establish a connection." + Environment.NewLine +
                                "Descriptions: " + ex.Message.ToString(), "C# Connect to SQL Server", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        
        }

        public static void closeConnection()
        {

            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            
            }
            catch (Exception)
            { 
            
                //
            
            }

      
        }

    }
}
