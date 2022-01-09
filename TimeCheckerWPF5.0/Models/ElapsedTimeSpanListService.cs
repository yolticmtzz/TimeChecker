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
        private readonly ObservableCollection<TimeSpanRecord> _elapsedTimeSpanList;
        public ObservableCollection<TimeSpanRecord> ElapsedTimeSpanList => _elapsedTimeSpanList;

        public ElapsedTimeSpanListService()
        {
            _elapsedTimeSpanList = new ObservableCollection<TimeSpanRecord>();

        }


        public void AddTimeSpanRecord(TimeSpanRecord timeSpanRecord)
        {
            ElapsedTimeSpanList.Add(timeSpanRecord);
        }

    }

}















