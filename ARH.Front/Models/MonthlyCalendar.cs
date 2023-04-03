namespace ARH.Front.Models
{
    public class MonthlyCalendar
    {
        public string UserName { get; set; }
        public IEnumerable<DayData> Days { get; set; }
        public MonthlyCalendar(string userName, int month, int year)
        {
            UserName = userName;
            List<DayData> lst = new ();
            for (int i = 1; i < 32 ; ++i)
            {
                try
                {
                    DateTime day = new DateTime(year, month, i);
                    lst.Add(new DayData { Day = day });
                } 
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            Days = lst;
        }
    }
}