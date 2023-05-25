namespace ARH.Front.Models
{
    public class MonthlyCalendar
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
        public List<DayData> Days { get; set; }   

        public MonthlyCalendar()
        {
            Comment = string.Empty;
            UserName = string.Empty;
            Days = new List<DayData>();        
            //jours= new joursdata[0];
        }

        public List<string> MonthName(int year, string userName)
        {
            UserName = userName;
            List<string> months = new List<string>();
            for (int i = 1; i <= 12; i++)

            {
                DateTime month = new DateTime(year, i, 1);
                months.Add(month.ToString("MMMM"));

            }
            return months;
        }
    }
}