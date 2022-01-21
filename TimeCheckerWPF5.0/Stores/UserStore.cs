using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Stores
{
    /// Summary:
    ///    A service that stores the current logged user. 
    ///    an only be changed through the loginViewModel
    public class UserStore
    {
        private Employee _currentUser;
        public Employee CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
            }
        }
    }

}
