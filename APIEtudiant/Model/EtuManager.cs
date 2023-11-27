﻿using APIEtudiant.Model.Enumerations;
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
        /// <param name="promo">Promo dont on veut les etudiants</param>
        /// <returns>Ensemble de tout les étudiants</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu(Promotion promo)
        {
            return EtuDAO.GetAllEtu(promo);
        }

        /// <summary>
        /// Ajoute un Etudiant
        /// </summary>
        /// <param name="etu">étudiant à ajouter</param>
        /// <returns>true si l'ajout a fonctionné</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etu,Promotion promo)
        {
            return EtuDAO.AddEtu(etu, promo);
        }

        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu, Promotion promo)
        {
            return EtuDAO.AddSeveralEtu(listeEtu, promo);
        }

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS et renvoi true, sinon renvoi false
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <returns>si l'ajout est un succès</returns>
        public bool CreateEtu(Etudiant etu,Promotion promo)
        {
            return EtuDAO.CreateEtu(etu,promo);
        }


        /// <summary>
        /// Renvoi tous les étudiants ayant au moins une note de la catégorie spécifiée dans la promo specifie
        /// </summary>
        /// <param name="categorie">Catégorie spécifiée</param>
        /// <param name="promo">Promo dans laquel on cherche les etudiants</param>
        /// <returns>Un dictionnaire(liste de Tuple) avec les étudiants et le nombre de notes de la catégorie</returns>
        /// <author>Nordine</author>
        public List<Tuple<Etudiant, int>> GetAllEtuByCategorie(CATEGORIE categorie, Promotion promo)
        {
            return EtuDAO.GetAllEtuByCategorie(categorie,promo);
        }



    }
}
