using Microsoft.EntityFrameworkCore;
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
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;
using TimeCheckerWPF5._0.Views;

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NavigationStore navigationStore;

        public MainWindow()//ApplicationDbContext context)
        {

            InitializeComponent();

            Application.Current.Exit += new ExitEventHandler(ExitApp);
        }


        private void ExitApp(object sender, ExitEventArgs e)
        {
            MessageBox.Show("TimeChecker was terminated");
        }
    }
}


//private void Insert(short type, string comment, string user)
//{

//    var record = new Timeentry()
//    {
//        Type = type,
//        DateTime = DateTime.Now,
//        Comment = comment,
//        User = user,
//    };

//    _context.Timeentry.Add(record);
//    _context.SaveChanges();

//}


//private void CheckInButton_OnClick(object sender, RoutedEventArgs e)
//{

//    //User -> from config file (XML)??. Can't add the System.Configuration.dll reference in 5.0...
//    //string sAttr = ConfigurationManager.AppSettings.Get("User");

//    //We need a BL object to access the BLL
//    //Check if user is checking in or checking out
//    if (CheckInButton.IsChecked == true)
//    {
//        /*Since the User is checking in, we create a start timeentry, access the BLL and hand over the user and check-type data,
//        /change status and start stopwatch,
//        /Then make the break buttons appear
//        */
//        try
//        {
//            Insert(1, "", "dummy");
//            StatusScreen.Text = "Checked In";
//            BreakButton.Visibility = Visibility.Visible;
//            BreakTimeWatch.Visibility = Visibility.Visible;
//        }
//        catch (Exception exception)
//        {
//            Console.WriteLine(exception);
//            throw;
//        }
//    }
//    else
//    {
//        /*Since the User is checking out, we disable the Checking-button and Break-Button and open a new Window, where the user can specify his/her work
//        /change status and stop stopwatch,
//        /Then make the break buttons disappear again
//         */
//        try
//        {
//            //catch a comment*
//            Insert(2, "Check Out Comment to be implemented", "dummy");
//            //CheckInButton.IsEnabled = false;
//            //BreakButton.IsEnabled = false;
//            StatusScreen.Text = "Checked Out";

//            BreakButton.Visibility = Visibility.Hidden;
//            BreakTimeWatch.Visibility = Visibility.Hidden;

//        }
//        catch (Exception exception)
//        {
//            Console.WriteLine(exception);
//            throw;
//        }
//    }
//}

//internal static void Insert()
//{
//    throw new NotImplementedException();
//}

//private void BreakButton_OnClick(object sender, RoutedEventArgs e)
//{
//    //User -> from config file (XML)??. Can't add the System.Configuration.dll reference in 5.0...
//    //string sAttr = ConfigurationManager.AppSettings.Get("User");

//    /*Since the User is pausing the TimeChecker, we create a start Break timeentry, access the BLL and hand over the user and check-type data,
//    /change status, stop the maintime stopwatch, start the break stopwatch and disable the Checkin-button
//    /
//    */
//    if (BreakButton.IsChecked == true)
//    {
//        try
//        {
//            Insert(3, "", "dummy");
//            StatusScreen.Text = "Check In paused";
//            CheckInButton.IsEnabled = false;
//        }
//        catch (Exception exception)
//        {
//            Console.WriteLine(exception);
//            throw;
//        }
//    }
//    else
//    {
//        /*Since the User is ending the break, we create a end Break timeentry, access the BLL and hand over the user and check-type data,
//        /change status and start stopwatch,
//        /Then make the break buttons disappear again
//         */
//        try
//        {
//            Insert(4, "", "dummy");
//            CheckInButton.IsEnabled = true;
//            StatusScreen.Text = "Checked In";
//        }
//        catch (Exception exception)
//        {
//            Console.WriteLine(exception);
//            throw;
//        }

//    }

//}


