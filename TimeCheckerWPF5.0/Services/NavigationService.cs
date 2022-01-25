using System;
using TimeCheckerWPF5._0.Exceptions;
using TimeCheckerWPF5._0.Stores;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Services
{

    /// <summary>
    ///     A service, that is provided to the application to handle navigation mechanisms
    ///     It uses the navigationStore and the serviceProvider from DI to set the
    ///     targeted ViewModel as the current View Model in the navigationStore, where
    ///     the datacontext's current viewmodel is defined.
    /// </summary>
    public class NavigationService : INavigationService
        
    {
        private readonly NavigationStore _navigationStore;
        private readonly IServiceProvider _serviceProvider;


        /// <summary>
        ///     Initializes a new instance of a NavigationService and
        ///     injects the navigationStore and the serviceProvider
        ///     
        /// <paramref name="navigationStore">
        ///     injects the navigationStore from DI
        /// </paramref>
        ///     injects the serviceProvider from DI
        /// <paramref name="serviceProvider">
        /// </paramref>
        /// 
        /// </summary> 
        public NavigationService(NavigationStore navigationStore, IServiceProvider serviceProvider)
        {
            _navigationStore = navigationStore;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///     Navigates to the targeted View based on setting the
        ///     targetes viewModel to the currentViewModel in the navigationStore
        ///     (thats how the datacontext in the MainView is bound)
        /// 
        /// Exceptions:
        ///     To avoid navigating to something else than a UI ViewModel, 
        ///     the navigationtarget's model is checked to be a ViewModel of
        ///     ViewModelBase. Because only UI ViewModels inherit from ViewModelBase.
        /// 
        /// <paramref name="type">
        ///     ir requires the type of ViewModel where it should navigate to.
        /// </paramref>
        /// 
        /// </summary> 
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
