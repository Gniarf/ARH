namespace ARH.Front.Models
{
    public class Holyday
    {
        /// <summary>
        /// Clé technique, sans intérêt métier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date du jour férier
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Indique s'il faut prendre en compte l'année dans Date ou pas (le jour de l'an est toujours le 1° janvier par exemple, peu importe l'année)
        /// </summary>
        public bool YearIncluded { get; set; }
    }
}