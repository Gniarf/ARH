using ARH.Front.Models;

namespace ARH.Front.Contracts
{
    public interface ICalendarService
    {
        MonthlyCalendar GetCalendar(CalendarRequest request);
        void SetCalendar(MonthlyCalendar currentCalendar);
    }
}