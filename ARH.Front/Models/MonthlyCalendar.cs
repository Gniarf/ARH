namespace ARH.Front.Models
{
    public class MonthlyCalendar
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime RequestDate { get; set; }
        public List<DayData> Days { get; set; }

        public MonthlyCalendar()
        {
            Comment = string.Empty;
            UserName = string.Empty;
            Days = new List<DayData>();
        }

        public List<string> MonthName()
        {
            List<string> months = new();
            for (int i = 1; i <= 12; i++)
            {
                DateTime month = new(2023, i, 1);
                months.Add(month.ToString("MMMM"));
            }
            return months;
        }
    }
}