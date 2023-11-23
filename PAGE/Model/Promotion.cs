﻿using PAGE.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    /// <summary>
    /// Promotion d'étudiants
    /// </summary>
    /// <author>Yamato</author>
    public class Promotion
    {
        private List<Etudiant> etudiants;
        private NOMPROMOTION nomPromotion;

        /// <summary>
        /// Renvoie ou définit les étudiants composant la promotion
        /// </summary>
        /// <author>Yamato</author>
        public List<Etudiant> Etudiants { get {  return etudiants; } set {  etudiants = value; } }

        /// <summary>
        /// Renvoie ou définit le nom de la promotion (premiere année, deuxieme année, troisieme année)
        /// </summary>
        /// <author>Yamato</author>
        public NOMPROMOTION NomPromotion { get { return nomPromotion; } set {  nomPromotion = value; } }

        /// <summary>
        /// Constructeur de Promotion
        /// </summary>
        /// <param name="etu">etudiants de la promotion</param>
        /// <param name="nomPromo">nom de la promotion (premiere année, deuxieme année, troisieme année)</param>
        /// <author>Yamato</author>
        public Promotion(List<Etudiant> etu, NOMPROMOTION nomPromo) 
        {
            this.etudiants = etu;
            this.nomPromotion = nomPromo;
        }
    }
}
