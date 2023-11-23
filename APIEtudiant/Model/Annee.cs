namespace APIEtudiant.Model
{
    /// <summary>
    /// Une année de BUT Informatique
    /// </summary>
    /// <author>Yamato</author>
    public class Annee
    {
        private int anneeDebut;
        private Promotion premiereAnnee;
        private Promotion deuxiemeAnnee;
        private Promotion troisiemeAnnee;

        /// <summary>
        /// Renvoie ou définit l'année de début 
        /// </summary>
        /// <author>Yamato</author>
        public int AnneeDebut { get { return anneeDebut; } set { anneeDebut = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de premiere année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion PremiereAnnee { get { return premiereAnnee; } set { premiereAnnee = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de deuxieme année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion DeuxiemeAnnee { get { return deuxiemeAnnee; } set { deuxiemeAnnee = value; } }

        /// <summary>
        /// Renvoie ou définit la promotion de troisieme année
        /// </summary>
        /// <author>Yamato</author>
        public Promotion TroisiemeAnnee { get { return troisiemeAnnee; } set { troisiemeAnnee = value; } }

        /// <summary>
        /// Constructeur d'année
        /// </summary>
        /// <param name="anneeDebut">année de début</param>
        /// <param name="premiereAnnee">promotion de premiere année</param>
        /// <param name="deuxiemeAnnee">promotion de deuxieme année</param>
        /// <param name="troisiemeAnnee">promotion de troisieme année</param>
        /// <author>Yamato</author>
        public Annee(int anneeDebut, Promotion premiereAnnee, Promotion deuxiemeAnnee, Promotion troisiemeAnnee)
        {
            this.anneeDebut = anneeDebut;
            this.premiereAnnee = premiereAnnee;
            this.deuxiemeAnnee = deuxiemeAnnee;
            this.troisiemeAnnee = troisiemeAnnee;
        }
    }
}
