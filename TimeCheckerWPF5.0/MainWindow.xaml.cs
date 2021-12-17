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
        private readonly ApplicationDbContext _context;

        public MainWindow(ApplicationDbContext context)
        {
            _context = context;
            InitializeComponent();
            GetTodoItems();
        }

        private void GetTodoItems()
        {
            var todoItems = _context.Timeentry.ToList();
            TodoItemGrid.ItemsSource = todoItems;
        }
    }
}
