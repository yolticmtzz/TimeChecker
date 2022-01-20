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


            services.AddSingleton<INavigationService>(s => CreateLoginNavigationService(s));

            services.AddSingleton<TimeCheckerViewModel>(s => new TimeCheckerViewModel(
                s.GetRequiredService<UserStore>(),
                s.GetRequiredService<ElapsedTimeSpanListStore>(),
                s.GetRequiredService<DataBaseService>()    
                )) ;

            services.AddTransient<ElapsedTimesViewModel>(s => new ElapsedTimesViewModel(
                s.GetRequiredService<ElapsedTimeSpanListStore>()
                ));

            services.AddSingleton<LoginViewModel>(CreateLoginViewModel);
            services.AddTransient<HeaderViewModel>(CreateHeaderViewModel);
            services.AddTransient<NavigationViewModel>(CreateNavigationViewModel);
            
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);

        }

        private HeaderViewModel CreateHeaderViewModel(IServiceProvider serviceProvider)
        {
            return new HeaderViewModel(serviceProvider.GetRequiredService<UserStore>());
        }

        private NavigationViewModel CreateNavigationViewModel(IServiceProvider serviceProvider)
        {
           return new NavigationViewModel(
           CreateLoginNavigationService(serviceProvider),
           CreateTimeCheckerNavigationService(serviceProvider),
           CreateElapsedTimeNavigationService(serviceProvider)
           );
        }

        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<LoginViewModel>(),
                () => serviceProvider.GetRequiredService<HeaderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationViewModel>()
                );
        }

        private INavigationService CreateTimeCheckerNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<TimeCheckerViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TimeCheckerViewModel>(),
                () => serviceProvider.GetRequiredService<HeaderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationViewModel>()
                );
        }

        private INavigationService CreateElapsedTimeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ElapsedTimesViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ElapsedTimesViewModel>(),
                () => serviceProvider.GetRequiredService<HeaderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationViewModel>()
                );
        }
        private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
        {

            LayoutNavigationService<TimeCheckerViewModel> navigationService = new LayoutNavigationService<TimeCheckerViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TimeCheckerViewModel>(),
                () => serviceProvider.GetRequiredService<HeaderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationViewModel>()
                ); 

            return new LoginViewModel(
                serviceProvider.GetRequiredService<UserStore>(),
                navigationService);
        }
    }
}
