using System;

namespace TimeCheckerWPF5._0.Services
{

    /// <summary>
    ///     An interface to implement a Navigate method. 
    ///     Any instance of NavigationService (whether layout or standard) will implement this interface and thus a method
    ///     called "Navigate". The purpose of a NavigationService is to offer a navigation
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     forces the implementation of a method called "Navigate" that should implement
        ///     a navigation logic
        /// </summary>
        void NavigateToType(Type type);

    }
}
