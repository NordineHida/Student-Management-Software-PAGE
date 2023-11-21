using APIEtudiant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEtudiant.Model
{
    /// <summary>
    /// un Etudiant en BUT Informatique
    /// </summary>
    /// <author>Nordine</author>
    public class Etudiant
    {
        #region Attributs

        private int numApogee;
        private string nom;
        private string prenom;
        private SEXE sexe;
        private string typeBac;
        private string mail;
        private GROUPE groupe;
        private bool estBoursier;
        private REGIME typeFormation;
        private DateTime dateNaissance;
        private string login;
        private long telFixe;
        private long telPortable;
        private string adresse;

        #endregion

        #region Propriétés
        /// <summary>
        /// Obtient ou définit le numéro d'apogée.
        /// </summary>
        /// <author>Nordine</author>
        public int NumApogee { get { return numApogee; } set { numApogee = value; } }

        /// <summary>
        /// Obtient ou définit le nom.
        /// </summary>
        /// <author>Nordine</author>
        public string Nom { get { return nom; } set { nom = value; } }

        /// <summary>
        /// Obtient ou définit le prénom.
        /// </summary>
        /// <author>Nordine</author>
        public string Prenom { get { return prenom; } set { prenom = value; } }

        /// <summary>
        /// Obtient ou définit le sexe.
        /// </summary>
        /// <author>Nordine</author>
        public SEXE Sexe { get { return sexe; } set { sexe = value; } }

        /// <summary>
        /// Obtient ou définit le type de baccalauréat.
        /// </summary>
        /// <author>Nordine</author>
        public string TypeBac { get { return typeBac; } set { typeBac = value; } }

        /// <summary>
        /// Obtient ou définit l'adresse e-mail.
        /// </summary>
        /// <author>Nordine</author>
        public string Mail { get { return mail; } set { mail = value; } }

        /// <summary>
        /// Obtient ou définit le groupe.
        /// </summary>
        /// <author>Nordine</author>
        public GROUPE Groupe { get { return groupe; } set { groupe = value; } }

        /// <summary>
        /// Obtient ou définit si l'étudiant est boursier.
        /// </summary>
        /// <author>Nordine</author>
        public bool EstBoursier { get { return estBoursier; } set { estBoursier = value; } }

        /// <summary>
        /// Obtient ou définit le type de formation.
        /// </summary>
        /// <author>Nordine</author>
        public REGIME TypeFormation { get { return typeFormation; } set { typeFormation = value; } }

        /// <summary>
        /// Obtient ou définit la date de naissance.
        /// </summary>
        /// <author>Nordine</author>
        public DateTime DateNaissance { get { return dateNaissance; } set { dateNaissance = value; } }

        /// <summary>
        /// Obtient ou définit le nom d'utilisateur.
        /// </summary>
        /// <author>Nordine</author>
        public string Login { get { return login; } set { login = value; } }

        /// <summary>
        /// Obtient ou définit le numéro de téléphone fixe.
        /// </summary>
        /// <author>Nordine</author>
        public long TelFixe { get { return telFixe; } set { telFixe = value; } }

        /// <summary>
        /// Obtient ou définit le numéro de téléphone portable.
        /// </summary>
        /// <author>Nordine</author>
        public long TelPortable { get { return telPortable; } set { telPortable = value; } }

        /// <summary>
        /// Obtient ou définit l'adresse.
        /// </summary>
        /// <author>Nordine</author>
        public string Adresse { get { return adresse; } set { adresse = value; } }


        #endregion

        #region Méthodes

        /// <summary>
        /// Constructeur de la classe Étudiant.
        /// </summary>
        /// <param name="numApogee">Le numéro d'apogée de l'étudiant. (8 chiffres)</param>
        /// <param name="nom">Le nom de l'étudiant. (100 caractères maximum)</param>
        /// <param name="prenom">Le prénom de l'étudiant. (100 caractères maximum)</param>
        /// <param name="sexe">Le sexe de l'étudiant. (M pour masculin, F pour féminin, A pour autre)</param>
        /// <param name="typeBac">Le type de baccalauréat de l'étudiant. (50 caractères maximum)</param>
        /// <param name="mail">L'adresse e-mail de l'étudiant. (150 caractères maximum)</param>
        /// <param name="groupe">Le groupe auquel l'étudiant est assigné. (50 caractères maximum)</param>
        /// <param name="estBoursier">Indique si l'étudiant est boursier ou non.</param>
        /// <param name="typeFormation">Le type de formation de l'étudiant. (150 caractères maximum)</param>
        /// <param name="dateNaissance">La date de naissance de l'étudiant.</param>
        /// <param name="login">Le nom d'utilisateur de l'étudiant. (30 caractères maximum)</param>
        /// <param name="telFixe">Le numéro de téléphone fixe de l'étudiant. (15 chiffres max)</param>
        /// <param name="telPortable">Le numéro de téléphone portable de l'étudiant.(15 chiffres max)</param>
        /// <param name="adresse">L'adresse de l'étudiant. (150 caractères maximum)</param>
        /// <author>Nordine</author>
        public Etudiant(int numApogee, string nom, string prenom, SEXE sexe, string typeBac, string mail, GROUPE groupe, bool estBoursier, REGIME typeFormation,
            DateTime dateNaissance, string login, long telFixe, long telPortable, string adresse)
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

        /// <summary>
        /// Renvoi le nom de l'étudiant
        /// </summary>
        /// <returns>le nom de l'étudiant</returns>
        /// <author>Nordine</author>
        public override string ToString()
        {
            return this.nom;
        }
        #endregion
    }
}