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
            calendar.Days = records.To().ToList();
            calendar.UserName = request.UserName;
            return calendar;
        }
        public IEnumerable<string> GetDistinctUsernames(CalendarRequest request)
        {
        
            IEnumerable<string> records = dbContext.DailyRecordCollection?.Select(x => x.UserId).Distinct().ToArray() ?? Array.Empty<string>();

            return records;
        }


        public void SetCalendar(MonthlyCalendar currentCalendar)
        {
            if (dbContext.DailyRecordCollection == null)
            {
                throw new NullReferenceException();
            }
            IEnumerable<DailyRecord> records = currentCalendar.Days.To(currentCalendar.UserName);
            foreach (DailyRecord record in records)
            {
                DailyRecord? found = dbContext.DailyRecordCollection.FirstOrDefault(x => x.Id == record.Id);
                if (found != null)
                {
                    dbContext.DailyRecordCollection.Remove(found);
                }
                dbContext.DailyRecordCollection.Add(record);
            }

            dbContext.SaveChanges();
        }
    }
}