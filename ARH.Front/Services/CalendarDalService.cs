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
            if (currentCalendar.Comment != null)
            {

                dbContext.CommentCollection.Add(new Comment { Date = currentCalendar.RequestDate, Text = currentCalendar.Comment, UserId = currentCalendar.UserName });


            }
            dbContext.SaveChanges();
        }

        public IEnumerable<Holyday> GetHolday(HolydayRequest request)
        {
            if (dbContext.HolydayCollection == null || dbContext.HolydayUserCollection == null)
            {
                throw new NullReferenceException();
            }
            IEnumerable<Holyday> result = dbContext.HolydayCollection.Where(x => x.YearIncluded == false || x.Date.Year == request.Year);
            var specificHolydays = from hu in dbContext.HolydayUserCollection
                                   join h in dbContext.HolydayCollection on hu.HolydayId equals h.Id
                                   where hu.UserId == request.UserId
                                   select h;
            return result.Concat(specificHolydays);
            
        }

        public void SetHolyday(HolydaySetRequest request)
        {
            if (dbContext.HolydayCollection == null || dbContext.HolydayUserCollection == null)
            {
                throw new NullReferenceException();
            }
            // création d'un jour férié
            if (request.HolydayId == default)
            {
                var entry = dbContext.HolydayCollection.Add(new Holyday { Date = request.Date, YearIncluded = request.YearIncluded, });
                AddHolydayUsers(entry.Entity.Id, request.UserIdCollection);
            }
            else
            {
                Holyday? h = dbContext.HolydayCollection.FirstOrDefault(x => x.Id == request.HolydayId);
                if (h == null)
                {
                    throw new ArgumentException("L'identifiant de jour férié n'est pas présent en base de données");
                }
                h.Date = request.Date;
                h.YearIncluded = request.YearIncluded;
                RemoveHolydayUsers(request.HolydayId);
                AddHolydayUsers(request.HolydayId, request.UserIdCollection);
            }
            dbContext.SaveChanges();
        }

        public void DeleteHolyday(int holydayId)
        {
            if (dbContext.HolydayCollection == null || dbContext.HolydayUserCollection == null)
            {
                throw new NullReferenceException();
            }
            if (holydayId == default)
            {
                throw new ArgumentException();
            }
            RemoveHolydayUsers(holydayId);
            Holyday? h = dbContext.HolydayCollection.FirstOrDefault(x => x.Id == holydayId);
            if (h != null)
            {
                dbContext.HolydayCollection.Remove(h);
            }
            dbContext.SaveChanges();
        }

        private void AddHolydayUsers(int holydayId, IEnumerable<string> userIdCollection)
        {
            if (dbContext.HolydayUserCollection == null)
            {
                throw new NullReferenceException();
            }
            if (userIdCollection.Any())
            {
                foreach (string userId in userIdCollection)
                {
                    dbContext.HolydayUserCollection.Add(new HolydayUser { UserId = userId, HolydayId = holydayId });
                }
            }
        }

        private void RemoveHolydayUsers(int holydayId)
        {
            if (dbContext.HolydayUserCollection == null)
            {
                throw new NullReferenceException();
            }
            var found = dbContext.HolydayUserCollection.Where(x => x.HolydayId == holydayId);
            if (found.Any())
            {
                dbContext.HolydayUserCollection.RemoveRange(found);
            }
        }
    }
}