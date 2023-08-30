using ARH.Front.Models;

namespace ARH.Front.Contracts
{
    public interface ICalendarService
    {
        MonthlyCalendar GetCalendar(CalendarRequest request);
        void SetCalendar(MonthlyCalendar currentCalendar);
       IEnumerable<string> GetDistinctUsernames(CalendarRequest request);

        public IEnumerable<Holyday> GetHolidaysForUser(HolydayRequest request);
        public void SetHolyday(HolydaySetRequest request);
    }
}