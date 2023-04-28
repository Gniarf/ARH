using ARH.Front.Contracts;
using ARH.Front.Models;

namespace ARH.Front.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarDalService calendarDalService;

        public CalendarService(ICalendarDalService calendarDalService)
        {
            this.calendarDalService = calendarDalService;
        }

        public MonthlyCalendar GetCalendar(CalendarRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            MonthlyCalendar result = calendarDalService.Get(request);
            return result;
            //return new MonthlyCalendar(request.UserName, request.Date.Month, request.Date.Year);
        }
    }
}