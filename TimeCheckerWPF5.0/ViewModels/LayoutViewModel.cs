using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// Summary:
    ///     This class abstracts 3 ViewModels into one, so that the ultimate UI can be shown and managed from here.
    ///         HeaderView - HeaderViewModel (always present)
    ///         NavigationView - NavigationViewModel (always present)
    ///         Whatever is currently shown as the View - ContentViewModel 
    ///            - LoginViewModel/LoginView,
    ///            - TimeCheckerView/ViewModel,
    ///            - ElapsedTimeViewModel/ElapsedTimeView
    ///            
    ///          This ViewModel inherits from ViewModelBase to implement the RaisePropertyChanged.       

    public class LayoutViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; }
        public HeaderViewModel HeaderViewModel { get; }
        public ViewModelBase ContentViewModel { get; }


        /// Summary:
        ///     Initializes a new instance of a LayoutViewModel and initializes 
        ///     NavigationViewModel, HeaderViewModel and the ContentViewModel.
        ///
        /// Parameters:
        ///   navigationViewModel:
        ///     injects the NavigationViewModel
        ///   headerViewModel:
        ///     injects the HeaderViewModel
        ///   contentViewModel :
        ///     injects the ContentViewModel as whatever is currently bound to the content
        public LayoutViewModel(NavigationViewModel navigationViewModel,
            HeaderViewModel headerViewModel,
            ViewModelBase contentViewModel)
        {
            NavigationViewModel = navigationViewModel;
            HeaderViewModel = headerViewModel;
            ContentViewModel = contentViewModel;
        }

    }
}
