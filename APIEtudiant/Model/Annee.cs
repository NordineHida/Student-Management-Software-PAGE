namespace APIEtudiant.Model
{
    /// <summary>
    /// Une année de BUT Informatique
    /// </summary>
    /// <author>Yamato</author>
    public class Annee
    {
        private int anneeDebut;
        private Promotion but1;
        private Promotion but2;
        private Promotion but3;

        /// <summary>
        /// Renvoie ou définit l'année de début 
        /// </summary>
        /// <author>Yamato</author>
        public int AnneeDebut { get { return anneeDebut; } set { anneeDebut = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de premiere année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion But1 { get { return but1; } set { but1 = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de deuxieme année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion But2 { get { return but2; } set { but2 = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de troisieme année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion But3 { get { return but3; } set { but3 = value; } }

        /// <summary>
        /// Constructeur d'année
        /// </summary>
        /// <param name="anneeDebut">année de début</param>
        /// <param name="but1">promotion de premiere année</param>
        /// <param name="but2">promotion de deuxieme année</param>
        /// <param name="but3">promotion de troisieme année</param>
        /// <author>Yamato</author>
        public Annee(int anneeDebut)
        {
            this.anneeDebut = anneeDebut;

        }
    }
}
