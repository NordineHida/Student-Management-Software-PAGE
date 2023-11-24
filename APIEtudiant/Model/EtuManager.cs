using APIEtudiant.Model.Enumerations;
using APIEtudiant.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Gère le DAO d'étudiant
    /// </summary>
    /// <author>Nordine</author>
    public class EtuManager 
    {
        #region Singleton

        private static EtuManager instance;

        /// <summary>
        /// Renvoi la seule instance d'EtuManager
        /// </summary>
        /// <author>Nordine</author>
        public static EtuManager Instance
        {
            get
            {
                if (instance == null) instance = new EtuManager();
                return instance;
            }
        }

        private EtuManager()
        {
        }

        #endregion

        //DAO d'étudiant (Permet de changer directement tout les DAO)
        private IEtuDAO EtuDAO => new EtudiantDAOOracle();

        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Ensemble de tout les étudiants</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu()
        {
            return EtuDAO.GetAllEtu();
        }

        /// <summary>
        /// Ajoute un Etudiant
        /// </summary>
        /// <param name="etu">étudiant à ajouter</param>
        /// <returns>true si l'ajout a fonctionné</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etu)
        {
            return EtuDAO.AddEtu(etu);
        }

        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu)
        {
            return EtuDAO.AddSeveralEtu(listeEtu);
        }

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS et renvoi true, sinon renvoi false
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <returns>si l'ajout est un succès</returns>
        public bool CreateEtu(Etudiant etu)
        {
            return EtuDAO.CreateEtu(etu);
        }


        /// <summary>
        /// Renvoi tout les étudiants de la BDD qui ont une note de la categorie donner et le nombre de note de cette categorie
        /// </summary>
        /// <returns>Un dictionnaire etudiant/nombre de note de cette categorie</returns>
        /// <author>Nordine</author>
        public List<Tuple<Etudiant, int>> GetAllEtuByCategorie(CATEGORIE categorie)
        {
            return EtuDAO.GetAllEtuByCategorie(categorie);
        }



    }
}
