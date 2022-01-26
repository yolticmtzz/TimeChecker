using System;
using TimeChecker.DAL.Models;

namespace TimeCheckerWPF5._0.Stores
{
    /// <summary>
    ///    A service that stores the current logged user. 
    ///    Can only be changed through the loginViewModel
    ///    Implements an Action CurrentUserChanged to
    ///    inform the NavigationViewModel that a User logged in.
    /// </summary>
    public class UserStore
    {
        public Action CurrentUserChanged;

        private Employee _currentUser;
        public Employee CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnCurrentUserChanged();
            }
        }

        /// <summary>
        /// Invokes the CurrentUserChanged Action
        /// when the CurrentUser property's value
        /// changes.
        /// </summary>
        private void OnCurrentUserChanged()
        {
            CurrentUserChanged?.Invoke();
        }
    }

}
