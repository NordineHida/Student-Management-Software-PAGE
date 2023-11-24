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
        /// Constructeur de Promotion
        /// </summary>
        /// <param name="etu">etudiants de la promotion</param>
        /// <param name="nomPromo">nom de la promotion (premiere année, deuxieme année, troisieme année)</param>
        /// <author>Yamato</author>
        public Promotion(List<Etudiant> Etudiants, NOMPROMOTION NomPromotion)
        {
            this.etudiants = Etudiants;
            this.nomPromotion = NomPromotion;
        }
    }
}
