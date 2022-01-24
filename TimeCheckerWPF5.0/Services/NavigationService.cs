using System;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Services
{
    public class NavigationService : INavigationService
        
    {
        private readonly NavigationStore _navigationStore;
        private readonly IServiceProvider _serviceProvider;


        public NavigationService(NavigationStore navigationStore, IServiceProvider serviceProvider)
        {
            _navigationStore = navigationStore;
            _serviceProvider = serviceProvider;
        }

        public void NavigateToType(Type type)
        {
            try
            {
                var model = _serviceProvider.GetService(type);

                if (model is ViewModelBase viewModelBase)
                {
                    _navigationStore.CurrentViewModel = viewModelBase;
                }

            }
            catch (Exception ex)
            {
                throw new NavigationException($"Type to navigate to is not of ViewModelBase type {ex.Message}", ex);
            }

        }


    }

}
