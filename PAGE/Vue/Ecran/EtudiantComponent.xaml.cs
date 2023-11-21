using PAGE.Model;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour EtudiantComponent.xaml
    /// </summary>
    public partial class EtudiantComponent : UserControl
    {

        public Etudiant Etudiant { get; set; }
        public int NumeroApogee { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Groupe { get; set; }

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

