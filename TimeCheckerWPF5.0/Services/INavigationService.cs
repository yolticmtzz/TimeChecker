namespace TimeCheckerWPF5._0.Services
{

    /// Summary:
    ///     An interface to implement a Navigate method. 
    ///     Any instance of NavigationService (whether layout or standard) will implement this interface and thus a method
    ///     called "Navigate". The purpose of a NavigationService is to offer a navigation
    public interface INavigationService
    {
        /// Summary:
        ///     forces the implementation of a method called "Navigate" that should implement
        ///     a navigation logic
        void Navigate();

    }
}
