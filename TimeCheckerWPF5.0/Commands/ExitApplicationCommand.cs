namespace TimeCheckerWPF5._0.Commands
{
    class ExitApplicationCommand : CommandBase
    {

        public override void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

