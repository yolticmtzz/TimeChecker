using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.Stores;

namespace TimeCheckerWPF5._0.ViewModels
{
    class HeaderViewModel : ViewModelBase
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