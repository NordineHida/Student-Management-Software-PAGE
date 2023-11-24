using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour EtudiantEtNoteComponent.xaml
    /// </summary>
    public partial class EtudiantEtNoteComponent : UserControl
    {
        /// <summary>
        /// etudiant attaché au component
        /// </summary>
        /// <author>Nordine</author>
        public Etudiant Etudiant { get; set; }

        /// <summary>
        /// Numero apogee de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public int NumeroApogee { get; set; }

        /// <summary>
        /// nom de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public string Nom { get; set; }

        /// <summary>
        /// prenom de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public string Prenom { get; set; }

        /// <summary>
        /// nombre de note de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public int NbNote { get; set; }

        /// <summary>
        /// groupe de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public string Groupe { get; set; }

        /// <summary>
        /// Construit un component pour afficher l'étudiant en parametre
        /// </summary>
        /// <param name="etu">etudiant qu'on affiche</param>
        /// <author>Nordine</author>
        public EtudiantEtNoteComponent(Etudiant etu, int nbNote)
        {
            InitializeComponent();
            DataContext = this;

            this.Etudiant = etu;
            this.NumeroApogee = Etudiant.NumApogee;
            //NOM Prenom
            this.Nom = Etudiant.Nom.ToUpper();
            this.Prenom = char.ToUpper(Etudiant.Prenom[0]) + Etudiant.Prenom.Substring(1).ToLower();
            this.NbNote = nbNote;
            this.Groupe = etu.Groupe.ToString();

        }
    }
}
