using ARH.Front.Contracts;
using ARH.Front.Models;

namespace ARH.Front.Services
{
    public class CalendarDalService : ICalendarDalService
    {
        private readonly DataContext dbContext;

        public CalendarDalService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public MonthlyCalendar Get(CalendarRequest request)
        {
            MonthlyCalendar calendar = new MonthlyCalendar();
            IEnumerable<DailyRecord> records = dbContext.DailyRecordCollection?
                    .Where(x => x.UserId == request.UserName && x.Day.Month == request.Date.Month && x.Day.Year == x.Day.Year).ToArray() 
                    ?? Array.Empty<DailyRecord>();
            calendar.Days = records.To();
            calendar.UserName = request.UserName;
            return calendar;
        }
    }
}