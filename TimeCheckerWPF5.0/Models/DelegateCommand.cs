using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeCheckerWPF5._0.Models
{
    public class DelegateCommand : ICommand
    {
        //DelegateCommand oder RelayCommand

        //Die konkrete Methodenimplemeniterung ist für jeden Aufrufer anders, deswegen wird das in delegates ausgelagert:
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public DelegateCommand(Predicate<object> canExecute, Action<object> execute) =>
            (this.canExecute, this.execute) = (canExecute, execute);

        public DelegateCommand(Action<object> execute) : this(null, execute) { }


        //Wenn dieses Event aufgerufen wird, evaluiert der Aufrufer erneute die CanExecute Methode.
        public event EventHandler CanExecuteChanged;

        //Um Funktion auszulösen:
        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        //Liefert, ob das Command überhaupt ausgeführt werden kann (Liefert z.B. False wenn der Button nicht geklickt wird.
        //Wenn true liefer = Button ist aktiviert).
        public bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter) ?? true;
        
    //Methode wird ausgeführt, wenn durch irgendwas aufgerufen wird
    public void Execute(object parameter) => this.execute?.Invoke(parameter);
    }
    
}
