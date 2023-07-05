namespace ARH.Front.Models
{
    /// <summary>
    /// Associations jour férié / utilisateur, destinée aux cas particuliers. 
    /// Un jour qui n'est associé à personne est appliqué à tout le monde.
    /// </summary>
    public class HolydayUser
    {
        /// <summary>
        /// Clé technique sans intérêt métier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// l'identifiant du jour férié rattaché à un utilisateur
        /// </summary>
        public int HolydayId { get; set; }
        /// <summary>
        /// l'identifiant de l'utilisateur concerné par le jour férié
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}