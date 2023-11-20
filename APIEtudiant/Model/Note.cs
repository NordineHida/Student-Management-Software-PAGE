﻿namespace APIEtudiant.Model
{
    /// <summary>
    /// les notes écrites par les différents acteurs de l'application à propos des étudiants
    /// </summary>
    /// <author>Laszlo/Nordine</author>
    public class Note
    {
        #region attributs
        private int idNote = 0;
        private string categorie;
        private DateTime datePublication;
        private string nature;
        private string commentaire;
        private int apogeeEtudiant;
        #endregion

        #region Propriétés


        /// <summary>
        /// Récupère ou définit l'id de la note. (par défaut 0)
        /// </summary>
        /// <author>Nordine</author>
        public int IdNote { get { return idNote; } set { idNote = value; } }


        /// <summary>
        /// Récupère ou définit la catégorie de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public string Categorie { get {  return categorie; } set {  categorie = value; } }

        /// <summary>
        /// Récupère ou définit la date de publication de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public DateTime DatePublication { get {  return datePublication; } set {  datePublication = value; } }
        
        /// <summary>
        /// Récupère ou définit la nature de la note.
        /// </summary>
        /// <author>Laszlo</author>
        public string Nature { get {  return nature; } set {  nature = value; } }

        /// <summary>
        /// Récupère ou définit le commentaire inscrit dans la note.
        /// </summary>
        /// <author>Laszlo</author>
        public string Commentaire { get {  return commentaire; } set {  commentaire = value; } }

        /// <summary>
        /// Récupère ou définit le numéro apogée de l'étudiant dont la note parle.
        /// </summary>
        /// <author>Laszlo</author>
        public int ApogeeEtudiant { get {  return apogeeEtudiant; } set {  apogeeEtudiant = value; } }

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
            /// <author>Laszlo</author>
        public Note(string categorie, DateTime datePublication,string nature,string commentaire, int apogeeEtudiant)
        {
            this.categorie = categorie;
            this.datePublication = datePublication;
            this.nature = nature;
            this.commentaire = commentaire;
            this.apogeeEtudiant= apogeeEtudiant;
        }

        #endregion 
    }
}
