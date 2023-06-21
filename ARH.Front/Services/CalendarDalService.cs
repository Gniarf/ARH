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
            if (dbContext.DailyRecordCollection == null || dbContext.CommentCollection == null)
            {
                throw new NullReferenceException();
            }
            MonthlyCalendar calendar = new MonthlyCalendar();
            IEnumerable<DailyRecord> records = dbContext.DailyRecordCollection
                    .Where(x => x.UserId == request.UserName && x.Day.Month == request.Date.Month && x.Day.Year == x.Day.Year).ToArray()
                    ?? Array.Empty<DailyRecord>();
            calendar.Days = records.To().ToList();
            calendar.UserName = request.UserName;
            calendar.RequestDate = request.Date;

            Comment? comment = dbContext.CommentCollection.FirstOrDefault(x => x.UserId == request.UserName && x.Date == request.Date);
            if (comment != null)
            {
                calendar.Comment = comment.Text;
            }
            
            return calendar;
        }
        public IEnumerable<string> GetDistinctUsernames(CalendarRequest request)
        {
            IEnumerable<string> records = dbContext.DailyRecordCollection?.Select(x => x.UserId).Distinct().ToArray() ?? Array.Empty<string>();
            return records;
        }

        public void SetCalendar(MonthlyCalendar currentCalendar)
        {
            if (dbContext.DailyRecordCollection == null || dbContext.CommentCollection == null)
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
            Comment? commentFound = dbContext.CommentCollection.FirstOrDefault(x => x.Date == currentCalendar.RequestDate 
                                                                                && x.UserId == currentCalendar.UserName);
            if (commentFound != null)
            {
                dbContext.CommentCollection.Remove(commentFound);
            }
            dbContext.CommentCollection.Add(new Comment { Date = currentCalendar.RequestDate, Text = currentCalendar.Comment, UserId = currentCalendar.UserName });

            dbContext.SaveChanges();
        }
    }
}