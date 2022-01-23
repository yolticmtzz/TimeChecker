using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeCheckerWPF5._0.ViewModels
{
    /// <summary>
    ///      A base class for all ViewModels to implement INotifyPropertyChanged's method RaisePropertyChanged,
    ///      which is the basic method every ViewModel needs to update the View's
    ///      
    ///      </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        
        /// <summary>
        ///     Occurs when a property value changes.
        ///     </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///      Invokes the PropertyChanged to notify the clients that a value has changed.
        ///      
        /// <paramref name="propertyName">
        /// the property that has changed as CallerMemberName, must be at least "" if null.
        /// </paramref>
        /// 
        /// </summary>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
