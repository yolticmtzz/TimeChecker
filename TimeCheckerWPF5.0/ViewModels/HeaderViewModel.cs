using System;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;
        public string UserFullName => $"{_userStore.CurrentUser?.Prename}, {_userStore.CurrentUser?.Lastname}";
        public string Date { get; set; }




        public HeaderViewModel(UserStore userStore)
        {

            _userStore = userStore;
            Date = DateTime.Now.ToLongDateString();

        }
    }
}