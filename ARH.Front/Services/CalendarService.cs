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

            // compléter result avec les jours non définis en bdd
            for (int i = 1; i <= DateTime.DaysInMonth(request.Date.Year, request.Date.Month); i++)
            {
                if (!result.Days.Any(day => day.Day.Day == i))
                {
                    DateTime newDay = new DateTime(request.Date.Year, request.Date.Month, i);
                    result.Days.Add(new DayData { Day = newDay });
                }
            }
            result.Days = result.Days.OrderBy(day => day.Day).ToList();
            return result;
        }
        public IEnumerable<string> GetDistinctUsernames(CalendarRequest request)
        {
            return calendarDalService.GetDistinctUsernames(request);
        }


        public void SetCalendar(MonthlyCalendar currentCalendar)
        {
            // si vérifications métier à faire, c'est ici, avant l'appel au service DAL
            calendarDalService.SetCalendar(currentCalendar);
        }
        public void SetHolyday(HolydaySetRequest request)
        {
            // si vérifications métier à faire, c'est ici, avant l'appel au service DAL
            calendarDalService.SetHolyday(request);
        }
        public void GetHolyday(HolydayRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<Holyday> result = calendarDalService.GetHolday(request);
        }
    }

}
