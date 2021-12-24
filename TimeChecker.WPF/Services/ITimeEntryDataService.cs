using TimeChecker.Domain.Models;
using TimeChecker.EntityFramework;

namespace TimeChecker.WPF.Services
{
    public interface ITimeEntryDataService
    {
        void CreateTimeEntry(int type);

    }
}
