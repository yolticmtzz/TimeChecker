using System;
using System.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeChecker.DAL.Data;
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

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationManager.ConnectionStrings
                    ["TimeCheckerDatabase"].ConnectionString);
            });

            services.AddSingleton<UserStore>();
            services.AddSingleton<ElapsedTimeSpanListStore>();
            services.AddSingleton<NavigationStore>();
            services.AddScoped<DataBaseService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<LoginViewModel>();
            services.AddSingleton<TimeCheckerViewModel>();
            services.AddTransient<ElapsedTimesViewModel>();
            services.AddTransient<NavigationViewModel>();
            services.AddSingleton<HeaderViewModel>();

            services.AddTransient<MainViewModel>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.NavigateToType(typeof(LoginViewModel));

            base.OnStartup(e);

        }

    }
}
