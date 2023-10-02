using APIEtudiant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    /// <summary>
    /// un Etudiant en BUT Informatique
    /// </summary>
    public class Etudiant
    {
        #region Attributs

        private int numApogee;
        private string nom;
        private string prenom;
        private SEXE sexe;
        private TYPEBAC typeBac;
        private string mail;
        private string groupe;
        private bool estBoursier;
        private TYPEFORMATION typeFormation;
        private DateTime dateNaissance;
        private string login;
        private int telFixe;
        private int telPortable;
        private string adresse;

        #endregion

        #region Méthodes

        /// <summary>
        /// Constructeur d'étudiant
        /// </summary>
        /// <param name="numApogee"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="sexe"></param>
        /// <param name="typeBac"></param>
        /// <param name="mail"></param>
        /// <param name="groupe"></param>
        /// <param name="estBoursier"></param>
        /// <param name="typeFormation"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="login"></param>
        /// <param name="telFixe"></param>
        /// <param name="telPortable"></param>
        /// <param name="adresse"></param>
        public Etudiant(int numApogee, string nom, string prenom, SEXE sexe, TYPEBAC typeBac, string mail, string groupe, bool estBoursier, TYPEFORMATION typeFormation,
            DateTime dateNaissance, string login, int telFixe, int telPortable, string adresse)
        {
            this.numApogee = numApogee;
            this.nom = nom;
            this.prenom = prenom;
            this.sexe= sexe;
            this.typeBac = typeBac;
            this.mail = mail;
            this.groupe = groupe;
            this.estBoursier = estBoursier;
            this.typeFormation = typeFormation;
            this.dateNaissance= dateNaissance;
            this.login = login;
            this.telFixe = telFixe;
            this.telPortable = telPortable;
            this.adresse = adresse;
        }

        //Faire toutes les get et set

        #endregion
    }
}