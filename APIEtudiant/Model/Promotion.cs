using APIEtudiant.Model.Enumerations;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Promotion d'étudiants
    /// </summary>
    /// <author>Yamato</author>
    public class Promotion
    {
        private List<Etudiant> etudiants = new List<Etudiant>();
        private NOMPROMOTION nomPromotion;
        private int anneeDebut;

        /// <summary>
        /// Renvoie ou définit les étudiants composant la promotion
        /// </summary>
        /// <author>Yamato</author>
        public List<Etudiant> Etudiants { get { return etudiants; } set { etudiants = value; } }

        /// <summary>
        /// Renvoie ou définit le nom de la promotion (premiere année, deuxieme année, troisieme année)
        /// </summary>
        /// <author>Yamato</author>
        public NOMPROMOTION NomPromotion { get { return nomPromotion; } set { nomPromotion = value; } }

        /// <summary>
        /// Renvoie ou définit l'année de début de la promotion
        /// </summary>
        /// <author>Yamato</author>
        public int AnneeDebut { get { return anneeDebut; } set { anneeDebut = value; } }

        /// <summary>
        /// Constructeur de Promotion
        /// </summary>
        /// <param name="Etudiants">etudiants de la promotion</param>
        /// <param name="NomPromotion">nom de la promotion (premiere année, deuxieme année, troisieme année)</param>
        /// <author>Yamato</author>
        public Promotion(NOMPROMOTION NomPromotion, int AnneeDebut)
        {
            this.nomPromotion = NomPromotion;
            this.anneeDebut = AnneeDebut;
        }
    }
}
