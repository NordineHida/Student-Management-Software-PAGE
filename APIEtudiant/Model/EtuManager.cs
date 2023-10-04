using APIEtudiant.Stockage;
using PAGE.Stockage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    public class EtuManager 
    {
        #region Singleton

        private static EtuManager instance;

        /// <summary>
        /// Renvoi la seule instance  d'EtuManager
        /// </summary>
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
        private IEtuDAO EtuDAO => EtudiantDAOOracle.Instance;

        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Ensemble de tout les étudiants</returns>
        public IEnumerable<Etudiant> GetAllEtu()
        {
            return EtuDAO.GetAllEtu();
        }

        /// <summary>
        /// Ajoute un Etudiant
        /// </summary>
        /// <param name="etu">étudiant à ajouter</param>
        /// <returns>true si l'ajout a fonctionné</returns>
        public bool AddEtu(Etudiant etu)
        {
            return EtuDAO.AddEtu(etu);
        }



    }
}
