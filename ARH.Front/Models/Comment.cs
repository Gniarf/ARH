namespace ARH.Front.Models
{
    public class Comment
    {
        /// <summary>
        /// Clé technique, sans intérêt métier.
        /// </summary>
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}