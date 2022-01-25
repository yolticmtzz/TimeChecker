using System;

namespace TimeCheckerWPF5._0.Services
{

    /// <summary>
    ///     An interface to implement a Navigate method. 
    ///     Any instance of NavigationService will implement this interface and thus a method
    ///     called "NavigateToType" to navigate to somewhere (usually another viewModel)
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     forces the implementation of a method called "NavigateToType" that should implement
        ///     a navigation logic
        ///     
        /// <paramref name="type">
        ///  The type of the ViewModel where to navigate to
        ///  </paramref>
        ///  
        /// </summary>
        void NavigateToType(Type type);

    }
}
