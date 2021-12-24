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
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationDbContext _context;
        private const string _loggedInUser = "DummyUser";
        
        public MainWindow()
        {

            InitializeComponent();
            SetUp();
            Application.Current.Exit += new ExitEventHandler(ExitApp);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainTimewatch = new TimeWatch();
            BreakTimewatch = new TimeWatch();

            //Subscribing the MainTimeWatch and the BreakTimeWatch to the TickEvent delegate
            MainTimewatch.TickEvent += MainTimewatchTriggered;
            BreakTimewatch.TickEvent += BreakTimewatchTriggered;
            //LoadDatagrid();
        }

        public TimeWatch MainTimewatch { get; set; }

        public TimeWatch BreakTimewatch { get; set; }


        public void SetUp()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);
        }

        private void Insert(short type, string comment, string user)
        {
            
            var record = new Timeentry()
            {
                Type = type,
                DateTime = DateTime.Now,
                Comment = comment,
                User = user,
            };

            _context.Timeentry.Add(record);
            _context.SaveChanges();

         }


            private void CheckInButton_OnClick(object sender, RoutedEventArgs e)
        {

            //User -> from config file (XML)??. Can't add the System.Configuration.dll reference in 5.0...
            //string sAttr = ConfigurationManager.AppSettings.Get("User");

            //We need a BL object to access the BLL
            //Check if user is checking in or checking out
            if (CheckInButton.IsChecked == true)
            {
                /*Since the User is checking in, we create a start timeentry, access the BLL and hand over the user and check-type data,
                /change status and start stopwatch,
                /Then make the break buttons appear
                */
                try
                {
                    Insert(1, "", _loggedInUser);
                    StatusScreen.Text = "Checked In";
                    MainTimewatch.StopwatchStart();
                    BreakButton.Visibility = Visibility.Visible;
                    BreakTimeWatch.Visibility = Visibility.Visible;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            else
            {
                /*Since the User is checking out, we disable the Checking-button and Break-Button and open a new Window, where the user can specify his/her work
                /change status and stop stopwatch,
                /Then make the break buttons disappear again
                 */
                try
                {
                    //catch a comment*
                    Insert(2, "Check Out Comment to be implemented", _loggedInUser);
                    //CheckInButton.IsEnabled = false;
                    //BreakButton.IsEnabled = false;
                    StatusScreen.Text = "Checked Out";
                    MainTimewatch.StopwatchReset();

                    BreakButton.Visibility = Visibility.Hidden;
                    BreakTimeWatch.Visibility = Visibility.Hidden;

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void BreakButton_OnClick(object sender, RoutedEventArgs e)
        {
            string user = "DummyUser";
            //User -> from config file (XML)??. Can't add the System.Configuration.dll reference in 5.0...
            //string sAttr = ConfigurationManager.AppSettings.Get("User");

            /*Since the User is pausing the TimeChecker, we create a start Break timeentry, access the BLL and hand over the user and check-type data,
            /change status, stop the maintime stopwatch, start the break stopwatch and disable the Checkin-button
            /
            */
            if (BreakButton.IsChecked == true)
            {
                try
                {
                    Insert(3, "", _loggedInUser);
                    StatusScreen.Text = "Check In paused";
                    MainTimewatch.StopwatchStop();
                    BreakTimewatch.StopwatchStart();
                    CheckInButton.IsEnabled = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            else
            {
                /*Since the User is ending the break, we create a end Break timeentry, access the BLL and hand over the user and check-type data,
                /change status and start stopwatch,
                /Then make the break buttons disappear again
                 */
                try
                {
                    Insert(4, "", _loggedInUser);
                    CheckInButton.IsEnabled = true;
                    StatusScreen.Text = "Checked In";
                    MainTimewatch.StopwatchStart();
                    BreakTimewatch.StopwatchStop();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }

            }

        }

        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The MainTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void MainTimewatchTriggered(object? sender, TickEventArgs e)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                e.TimeSpan.Hours, e.TimeSpan.Minutes, e.TimeSpan.Seconds);
            TimeWatch.Text = CurrentTime;
        }

        //Access the Timewatch Events to trigger, since its subscribed to the delegate
        // -> The BreakTimeWatch Textbox is to be updated with as a running timewatch in the defined DispatchTimers interval
        private void BreakTimewatchTriggered(object? sender, TickEventArgs e)
        {
            var CurrentTime = String.Format("{0:00}:{1:00}:{2:00}",
                e.TimeSpan.Hours, e.TimeSpan.Minutes, e.TimeSpan.Seconds);
            BreakTimeWatch.Text = CurrentTime;
        }

        private void ExitApp(object sender, ExitEventArgs e)
        {
            MessageBox.Show("TimeChecker wurde beendet.");
        }
    }
}






//        private void LoadDatagrid()
//        {
//            var timeentryitems = _context.Timeentry.ToList();
//            TimeentryGrid.ItemsSource = timeentryitems;
//        }

//        public void clearData()
//        {
//            Type_txt.Clear();
//            Comment_txt.Clear();
//            User_txt.Clear();
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            clearData();
//        }


//        private void Insert(object sender, RoutedEventArgs e)
//        {
//            short Type_int = Int16.Parse(Type_txt.Text);

//            var record = new Timeentry()
//            {
//                Type = Type_int,
//                DateTime = currentDate,
//                Comment = Comment_txt.Text,
//                User = User_txt.Text,
//            };

//            _context.Timeentry.Add(record);

//            _context.SaveChanges();

//           // _TimeentryId = record.ID;

//            LoadDatagrid();

//        }


//        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
//        {
//            int ID_int = Int32.Parse(ID_txt.Text);

//            var existing = _context.Timeentry.Single(x => x.ID == ID_int);
//            //var existing = _context.Timeentry.Single(x => x.Type == 2);

//            _context.Timeentry.Remove(existing);

//            _context.SaveChanges();

//            LoadDatagrid();
//        }


//        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
//        {
//            int ID_int = Int32.Parse(ID_txt.Text);
//            short Type_int = Int16.Parse(Type_txt.Text);

//            var existing = _context.Timeentry.Single(x => x.ID == ID_int);


//            existing.Type = Type_int;
//            existing.DateTime = currentDate;
//            existing.Comment = Comment_txt.Text;
//            existing.User = User_txt.Text;

//            _context.SaveChanges();

//            LoadDatagrid();

//        }
//    }
//}




//public void InsertTimeentry()
//{
//    var record = new Timeentry()
//    {
//        Type = 6,
//        Comment = "Organize meeting to discuss the project"
//    };

//    _context.Timeentry.Add(record);

//    _context.SaveChanges();

//}