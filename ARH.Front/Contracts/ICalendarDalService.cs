using ARH.Front.Models;

namespace ARH.Front.Contracts
{
    public interface ICalendarDalService
    {
        MonthlyCalendar Get(CalendarRequest request);
    }
}