using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCheckerWPF5._0.Models
{
    public class ElapsedTimeSpanListService
    {
        private readonly ObservableCollection<ElapsedTimeSpanListService> _elapsedTimeSpans;
        public ObservableCollection<ElapsedTimeSpanListService> ElapsedTimeSpans => _elapsedTimeSpans;

        public ElapsedTimeSpanListService()
        {
            _elapsedTimeSpans = new ObservableCollection<ElapsedTimeSpanListService>();

        }


        public void AddMainTimeSpan(TimeSpan timeSpan)
        {
            ElapsedTimeSpans.Add(timeSpan);
        }


        //public void AddBreakTimeSpan()
        //{
        //    ElapsedBreakTimeSpans.Add(CurrentTimeSpan);
        //}


    }
}
