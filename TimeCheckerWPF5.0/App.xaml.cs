using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeChecker.DAL.Data;
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
        private readonly NavigationStore _navigationStore;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            _navigationStore = new NavigationStore();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationManager.ConnectionStrings
                    ["TimeCheckerDatabase"].ConnectionString);
            });

            services.AddSingleton(typeof(NavigationStore));
            services.AddSingleton(typeof(MainWindow));

        }

        protected override void OnStartup(StartupEventArgs e) //(object sender, StartupEventArgs e)
        {
         //   _navigationStore.CurrentViewModel = new TimeCheckerViewModel();

            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel(_navigationStore)
            //};
            //MainWindow.Show();

            base.OnStartup(e);


            NavigationStore navigationStore = _serviceProvider.GetService<NavigationStore>();
            navigationStore.CurrentViewModel = new TimeCheckerViewModel();

            var mainWindow = _serviceProvider.GetService<MainWindow>();

            MainViewModel mainViewModel = new MainViewModel(navigationStore);
            mainViewModel.NavigationViewModel = new NavigationViewModel(navigationStore);
            mainWindow.DataContext = mainViewModel;
            
            mainWindow.Show();
            
        }


    }
}
