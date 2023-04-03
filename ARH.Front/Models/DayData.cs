namespace ARH.Front.Models
{
    public class DayData
    {
        /// <summary>
        /// Identification du jour, la partie Time est inutilisée
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// <c>true</c> si le jour est férié, <c>false</c> sinon
        /// </summary>
        public bool Free { get; set; }
        /// <summary>
        /// temps travaillé dans les locaux
        /// </summary>
        public Length OnSite { get; set; }
        /// <summary>
        /// temps passé en télétravail
        /// </summary>
        public Length AtHome { get; set; }
        /// <summary>
        /// temps passé en congés payés
        /// </summary>
        public Length PayedVacation { get; set; }
        /// <summary>
        /// temps passé en absence non payée
        /// </summary>
        public Length UnpayedVacation { get; set; }
        /// <summary>
        /// temps passé en arrêt maladie
        /// </summary>
        public Length Sickness { get; set; }
    }
}