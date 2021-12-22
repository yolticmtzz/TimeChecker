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

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            SetUp();
            InsertTimeentry();
            GetTimeentry();


        }


        public void SetUp()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);
        }

        private void GetTimeentry()
        {
            var todoItems = _context.Timeentry.ToList();
            TimeentryGrid.ItemsSource = todoItems;
        }


        public void InsertTimeentry()
        {
            var record = new Timeentry()
            {
                Type = 6,
                Comment = "Organize meeting to discuss the project"
            };

            _context.Timeentry.Add(record);

            _context.SaveChanges();

        }


    }
}

