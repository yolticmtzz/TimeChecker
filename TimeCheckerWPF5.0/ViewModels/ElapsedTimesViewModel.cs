using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeCheckerWPF5._0.Models;

namespace TimeCheckerWPF5._0.ViewModels
{
    public class ElapsedTimesViewModel : ViewModelBase
    {

        //private readonly ObservableCollection<ElapsedTimeSpanList> _elapsedMainTimeSpans;
        //public ObservableCollection<ElapsedTimeSpanList> ElapsedMainTimeSpans => _elapsedMainTimeSpans;
       
        private readonly ObservableCollection<ElapsedTimeSpanListService> _elapsedTimeList;
        public ObservableCollection<ElapsedTimeSpanListService> ElapsedTimeSpanList => _elapsedTimeList;


        public ElapsedTimesViewModel(ObservableCollection<ElapsedTimeSpanListService> elapsedTimeSpan)
        {
            _elapsedTimeList = elapsedTimeSpan;

        }

    }
}
