using System;
using System.Configuration;
using System.Windows;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeChecker.DAL.Data;
using TimeCheckerWPF5._0.Models;
using TimeCheckerWPF5._0.Services;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
      
        
        internal ApplicationDbContext _context = new(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TimeChecker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options);

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationManager.ConnectionStrings
                    ["TimeCheckerDatabase"].ConnectionString);
            });

            services.AddSingleton(typeof(NavigationStore));
            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton(typeof(ElapsedTimeSpanListStoreService));
            services.AddSingleton(typeof(UserStore));


        }

        protected override void OnStartup(StartupEventArgs e) 
        {
            NavigationStore navigationStore = _serviceProvider.GetService<NavigationStore>();
            ElapsedTimeSpanListStoreService elapsedTimeSpanListService = _serviceProvider.GetService<ElapsedTimeSpanListStoreService>();
            UserStore userStore = _serviceProvider.GetService<UserStore>();
            //NavigationsBarViewModel navigationsBarViewModel = new NavigationsBarViewModel


            //elapsedTimeSpanListService = new ElapsedTimeSpanListStoreService();
            navigationStore.CurrentViewModel = new LoginViewModel(userStore, navigationStore, elapsedTimeSpanListService);

            var mainWindow = _serviceProvider.GetService<MainWindow>();

            MainViewModel mainViewModel = new(navigationStore);
            mainViewModel.NavigationViewModel = new NavigationViewModel(CreateNavigationService());
            mainWindow.DataContext = mainViewModel;
            
            mainWindow.Show();

            base.OnStartup(e);

        }

        private NavigationService<TimeCheckerViewModel> CreateNavigationService()
        {
            return new NavigationService<TimeCheckerViewModel>(navigationStore, () => new TimeCheckerViewModel(userStore, elapsedTimeSpanListService));
        }
    }
}
