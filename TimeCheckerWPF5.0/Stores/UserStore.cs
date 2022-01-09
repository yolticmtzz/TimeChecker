using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Stores
{
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
