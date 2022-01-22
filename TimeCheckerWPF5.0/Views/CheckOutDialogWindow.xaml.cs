using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeCheckerWPF5._0.ViewModels;

namespace TimeCheckerWPF5._0.Views
{
    /// <summary>
    ///     Purpose of this View is explained in its ViewModel. It is a pop-up window for a
    ///     user interaction.
    ///     The event handlers are registered here, because they have to catch the return of the ShowDialog()
    ///     method of the window. All of this is called from the TimeCheckerViewModel and the TimeCheckerViewModel
    ///     further refines the result from the PopUp Window in its logic.///     
    /// </summary>
    public partial class CheckOutDialogWindow : Window
    {


        /// <summary>
        /// Initializes a new instance of a CheckOutDialogWindow and locally declares a variable 
        /// for the datacontext of its ViewModel, where it registers the CheckOut & Cancel ButtonClick Eventhandlers.
        /// </summary>
        public CheckOutDialogWindow()
        {
            InitializeComponent();
            var vm = DataContext as CheckOutDialogViewModel;
            vm.OnCheckOutClickEvent += HandleOnCheckOutClickEvent;
            vm.OnCancelClickEvent += HandleOnCancelClickEvent;
        }

        /// <summary>
        ///     Handles the HandleOnCheckOutClickEvent:
        ///     Sets the DialogResult = true
        ///
        /// <paramref name="sender">
        /// the "CheckOut" button clicked on the UI to run the CheckOutCommand
        /// </paramref>
        /// 
        /// <paramref name="e">
        /// No purpuse here, its empty
        /// </paramref>
        /// 
        /// </summary>
        private void HandleOnCheckOutClickEvent(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        ///     Handles the HandleOnCancelClickEvent:
        ///     Sets the DialogResult = false
        ///
        /// <paramref name="sender">
        /// the "Cancel" button clicked on the UI to run the CancelCommand
        /// </paramref>
        /// 
        /// <paramref name="e">
        /// No purpuse here, its empty
        /// </paramref>
        /// 
        /// </summary>
        private void HandleOnCancelClickEvent(object sender, EventArgs e)
        {
            DialogResult = false;
        }

    }
}
