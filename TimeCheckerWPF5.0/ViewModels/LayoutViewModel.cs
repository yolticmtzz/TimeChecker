using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; }
        public HeaderViewModel HeaderViewModel { get; }
        public ViewModelBase ContentViewModel { get; }

        public LayoutViewModel(NavigationViewModel navigationViewModel,
            HeaderViewModel headerViewModel,
            ViewModelBase contentViewModel)
        {
            NavigationViewModel = navigationViewModel;
            HeaderViewModel = headerViewModel;
            ContentViewModel = contentViewModel;
        }

        //public override void Dispose()
        //{
        //    NavigationBarViewModel.Dispose();
        //    ContentViewModel.Dispose();

        //    base.Dispose();
        //}
    }
}
