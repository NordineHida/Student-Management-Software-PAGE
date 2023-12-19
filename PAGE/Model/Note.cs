using PAGE.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    /// <summary>
    /// les notes écrites par les différents acteurs de l'application à propos des étudiants
    /// </summary>
    /// <author>Laszlo/nordine</author>
    public class Note
    {
        #region attributs
        private int idNote=0;
        private CONFIDENTIALITE confidentialite;
        private CATEGORIE categorie;
        private DateTime datePublication;
        private NATURE nature;
        private string commentaire;
        private string titre;
        private int apogeeEtudiant;
        #endregion

        #region Propriétés

        /// <summary>
        /// Récupère ou définit l'id de la note. (par défaut 0)
        /// </summary>
        /// <author>Nordine</author>
        public int IdNote { get { return idNote; } set { idNote = value; } }



        /// <summary>
        /// Récupère ou définit la confidentialite de la note.
        /// </summary>
        /// <author>Nordine</author>
        public CONFIDENTIALITE Confidentialite { get { return confidentialite; } set { confidentialite = value; } }


        /// <summary>
        /// Récupère ou définit la catégorie de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public CATEGORIE Categorie { get { return categorie; } set { categorie = value; } }

        /// <summary>
        /// Récupère ou définit la date de publication de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public DateTime DatePublication { get { return datePublication; } set { datePublication = value; } }

        /// <summary>
        /// Récupère ou définit la nature de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public NATURE Nature { get { return nature; } set { nature = value; } }

        /// <summary>
        /// Récupère ou définit le commentaire inscrit dans la note.
        /// </summary>
        /// <author>Laszlo</author>
        public string Commentaire { get { return commentaire; } set { commentaire = value; } }

        /// <summary>
        /// Récupère ou définit le titre de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public string Titre { get { return titre; } set { titre = value; } }

        /// <summary>
        /// Récupère ou définit le numéro apogée de l'étudiant dont la note parle.
        /// </summary>
        /// <author>Laszlo</author>
        public int ApogeeEtudiant { get { return apogeeEtudiant; } set { apogeeEtudiant = value; } }

        #endregion

        #region Méthodes 

        /// <summary>
        /// Constructeur de Notes
        /// </summary>
        /// <param name="categorie">catégorie de la note (50 caractères maximum)</param>
        /// <param name="datePublication">date de publication de la note</param>
        /// <param name="nature">Nature de la note (50 caractères maximum)</param>
        /// <param name="commentaire">Commentaire inscrit à l'intérieur de la note (255 caractères maximum)</param>
        /// <param name="apogeeEtudiant">Numéro apogée de l'étudiant dont la note parle</param>
        /// <param name="confidentialite">Confidentialite de la note</param>
        /// <author>Laszlo/Nordine</author>
        public Note(CATEGORIE categorie, string titre, DateTime datePublication, NATURE nature, string commentaire, int apogeeEtudiant, CONFIDENTIALITE confidentialite)
        {
            this.categorie = categorie;
            this.datePublication = datePublication;
            this.nature = nature;
            this.commentaire = commentaire;
            this.titre = titre;
            this.apogeeEtudiant = apogeeEtudiant;
            this.confidentialite = confidentialite;
        }

        #endregion 
    }
}
