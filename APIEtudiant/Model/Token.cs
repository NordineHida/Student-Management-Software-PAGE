namespace APIEtudiant.Model
{
    /// <summary>
    /// Tokens déterminant si l'action est possible ou s'il faut se reconnevter
    /// </summary>
    public class Token
    {
        private Utilisateur userToken;
        private DateTime dateExpiration;

        /// <summary>
        /// Utilisateur visé par le token
        /// </summary>
        public Utilisateur UserToken { get {  return userToken; } set { userToken = value; } }
        
        /// <summary>
        /// Date à laquelle le token s'expire
        /// </summary>
        public DateTime DateExpiration { get {  return dateExpiration; } set {  dateExpiration = value; } }

        /// <summary>
        /// Crée un token à partir d'un utilisateur et d'une date d'expiration
        /// </summary>
        /// <param name="userToken">user visé par le token</param>
        /// <param name="dateExpiration">date d'expiration du token</param>
        public Token(Utilisateur userToken, DateTime dateExpiration)
        {
            this.userToken = userToken;
            this.dateExpiration = dateExpiration;
        }
    }
}
