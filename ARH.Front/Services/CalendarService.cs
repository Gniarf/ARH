using ARH.Front.Contracts;
using ARH.Front.Models;

namespace ARH.Front.Services
{
    public class CalendarService : ICalendarService
    {
        public MonthlyCalendar GetCalendar(CalendarRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            return new MonthlyCalendar(request.UserName, request.Date.Month, request.Date.Year);
        }
    }
}