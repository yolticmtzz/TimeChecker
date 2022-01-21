using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// Summary:
    ///      A base class for all ViewModels to implement INotifyPropertyChanged's method RaisePropertyChanged,
    ///      which is the basic method every ViewModel needs to update the View's
    public class ViewModelBase : INotifyPropertyChanged
    {
        
        /// Summary:
        ///     Occurs when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;

        /// Summary:
        ///      Invokes the PropertyChanged to notify the clients that a value has changed.
        ///      
        /// Parameters:
        ///   propertyName:
        ///     the property that has changed as CallerMemberName, must be at least "" if null.
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
