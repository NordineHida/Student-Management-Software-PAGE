using PAGE.Model;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour EtudiantComponent.xaml
    /// </summary>
    public partial class EtudiantComponent : UserControl
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
        /// groupe de l'étudiant
        /// </summary>
        /// <author>Nordine</author>
        public string Groupe { get; set; }

        /// <summary>
        /// Construit un component pour afficher l'étudiant en parametre
        /// </summary>
        /// <param name="etu">etudiant qu'on affiche</param>
        /// <author>Nordine</author>
        public EtudiantComponent(Etudiant etu)
        {
            InitializeComponent();
            DataContext = this;

            this.Etudiant = etu;
            this.NumeroApogee = etu.NumApogee;
            //NOM Prenom
            this.Nom = etu.Nom.ToUpper();
            this.Prenom = char.ToUpper(etu.Prenom[0]) + etu.Prenom.Substring(1).ToLower();
            this.Groupe = etu.Groupe.ToString();

        }
    }
}

