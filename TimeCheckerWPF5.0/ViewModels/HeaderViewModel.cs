using System;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// Summary:
    ///     Represents and handles the data presented to the UI by the HeaderView.
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    /// 
    ///     It reflects a simple banner that shows the logged in user and the current date.
    ///     The ViewModel and the corresponding view are constantly presented in the UI
    ///     and exist exactly once
    public class HeaderViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;
        public string UserFullName => _userStore.CurrentUser?.Fullname;
        public string Date { get; set; }


        /// Summary:
        ///     Initializes a new instance of the HeaderViewModel
        ///     and uses the userstore's CurrentUser to present it in the view.
        ///     Also sets the date based on actual system time and presents it in 
        ///     a certain format to the UI.
        ///
        /// Parameters:
        ///   userStore:
        ///     injects the UserStore
        public HeaderViewModel(UserStore userStore)
        {
            _userStore = userStore;
            Date = DateTime.Now.ToLongDateString();

        }
    }
}