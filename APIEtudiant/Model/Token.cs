namespace APIEtudiant.Model
{
    /// <summary>
    /// Tokens déterminant si l'action est possible ou s'il faut se reconnevter
    /// </summary>
    public class Token
    {
        private int idToken;
        private DateTime dateExpiration;

        public int IdToken { get {  return idToken; } set {  idToken = value; } }
        public DateTime DateExpiration { get {  return dateExpiration; } set {  dateExpiration = value; } }

        public Token(int idToken, DateTime dateExpiration) 
        { 
            this.idToken = idToken;
            this.dateExpiration = dateExpiration;   
        }
    }
}
