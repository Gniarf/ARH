namespace ARH.Front.Models
{
    public class DailyRecord
    {
        /// <summary>
        /// Clé technique, sans intérêt métier.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Identification du jour, la partie Time est inutilisée
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// temps travaillé dans les locaux
        /// </summary>
        public string OnSite { get; set; } = Length.O.ToString();
        /// <summary>
        /// temps passé en télétravail
        /// </summary>
        public string AtHome { get; set; } = Length.O.ToString();
        /// <summary>
        /// temps passé en congés payés
        /// </summary>
        public string PayedVacation { get; set; } = Length.O.ToString();
        /// <summary>
        /// temps passé en absence non payée
        /// </summary>
        public string UnpayedVacation { get; set; } = Length.O.ToString();
        /// <summary>
        /// temps passé en arrêt maladie
        /// </summary>
        public string Sickness { get; set; } = Length.O.ToString();
        /// <summary>
        /// identifiant de l'utilisateur concerné par la saisie
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}