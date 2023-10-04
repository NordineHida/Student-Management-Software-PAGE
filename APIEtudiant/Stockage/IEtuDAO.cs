﻿using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    /// <summary>
    /// Interface de DAO des étudiants
    /// </summary>
    public interface IEtuDAO
    {
        /// <summary>
        /// Renvoi tout les étudiants
        /// </summary>
        /// <returns>Un ensemble d'étudiant</returns>
        public IEnumerable<Etudiant> GetAllEtu();

        /// <summary>
        /// Ajoute un étudiant
        /// </summary>
        /// <param name="etudiant">étudiant à ajouter</param>
        /// <returns>true si l'ajout a fonctionner</returns>
        public bool AddEtu(Etudiant etudiant);
    }
}
