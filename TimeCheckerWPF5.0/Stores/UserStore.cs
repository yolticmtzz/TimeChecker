using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Stores
{
    /// <summary>
    ///    A service that stores the current logged user. 
    ///    an only be changed through the loginViewModel
    /// </summary>
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
