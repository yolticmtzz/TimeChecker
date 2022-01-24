using System;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// <summary>
    ///     Represents and handles the data presented to the UI by the HeaderView.
    ///     
    ///     This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.
    /// 
    ///     It reflects a simple banner that shows the logged in user and the current date.
    ///     The ViewModel and the corresponding view are constantly presented in the UI
    ///     and exist exactly once
    ///     </summary>
    public class HeaderViewModel : ViewModelBase
    {


        private string userFullName;

        public string UserFullName
        {
            get => userFullName;

            set
            {
                userFullName = value;
                RaisePropertyChanged();

            }
        }
    
        public string Date { get; set; }


        /// <summary>
        ///     Initializes a new instance of the HeaderViewModel
        ///     and uses the userstore's CurrentUser to present it in the view.
        ///     Also sets the date based on actual system time and presents it in 
        ///     a certain format to the UI.
        ///
        /// <paramref name="userStore">
        ///     injects the UserStore
        ///     </paramref>
        ///     
        ///     </summary>
        public HeaderViewModel(UserStore userStore)
        {
            Date = DateTime.Now.ToLongDateString();
            
            if (userStore.CurrentUser != null)
            {
                UserFullName = userStore.CurrentUser.Fullname;
                return;
            }

            UserFullName = "Login required";

        }
    }
}