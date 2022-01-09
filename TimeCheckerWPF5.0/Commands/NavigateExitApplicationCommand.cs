using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.Stores
{
    class NavigateExitApplicationCommand : CommandBase
    {
   
        public override void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

