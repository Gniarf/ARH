namespace ARH.Front.Models
{
    public class HolydaySetRequest
    {
        public int HolydayId { get; set; }
        public DateTime Date { get; set; }
        public bool YearIncluded { get; set; }
        public IEnumerable<string> UserIdCollection { get; set; } = new string[0];
    }
}