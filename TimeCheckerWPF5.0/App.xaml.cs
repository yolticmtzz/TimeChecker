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
    ///     Interaction logic for App xaml:
    ///     Setting Up Dependency Injection with the
    ///     required services as a IServiceCollection

    ///     Overriting the StartUp Method, 
    ///     to create our own Start Up scenario
    ///     
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        ///       Initializing and building up the IServiceCollection
        ///       of the DI with the relevant service within their
        ///       required scope (singleton, transient or scoped)
        /// </summary>
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

        /// <summary>
        ///       This overrides the OnStartup Event of the WPF application
        ///       to start up with the DI services as well as
        ///       using the MainWindow as a service. Furthermore the "LoginViewModel"
        ///       is bound to the shown CurrentViewModel DataContext in the MainView
        ///       after startup, by injecting a NavigationService of type "LoginViewModel".
        ///       (Review the Navigation logic for more details)
        /// 
        /// <paramref name="e"/>
        ///     the StartupEventArgs
        /// </summary>
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
