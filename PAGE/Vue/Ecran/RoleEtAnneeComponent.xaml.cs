using PAGE.Model.Enumerations;
using System.Windows.Controls;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour RoleEtAnneeComponent.xaml
    /// </summary>
    public partial class RoleEtAnneeComponent : UserControl
    {
        /// <summary>
        /// intitule du role du component
        /// </summary>
        /// <author>Nordine</author>
        public string RoleString { get; set; }

        /// <summary>
        /// Role du component
        /// </summary>
        public ROLE Role { get; set; }
        /// <summary>
        /// Annee du component
        /// </summary>
        /// <author>Nordine</author>
        public int Annee { get; set; }


        /// <summary>
        /// constructeur de component
        /// </summary>
        /// <param name="annee">année à afficher</param>
        /// <param name="role">role à afficher</param>
        /// <author>Nordine</author>
        public RoleEtAnneeComponent(int annee, ROLE role)
        {
            InitializeComponent();
            switch (role)
            {
                case ROLE.DIRECTEURDEPARTEMENT:
                    RoleString = "Directeur de département";
                    break;
                case ROLE.DIRECTEURETUDES1:
                    RoleString = "Directeur des études (BUT1)";
                    break;
                case ROLE.DIRECTEURETUDES2:
                    RoleString = "Directeur de département (BUT2)";
                    break;
                case ROLE.DIRECTEURETUDES3:
                    RoleString = "Directeur de département (BUT3)";
                    break;
                case ROLE.ADMIN:
                    RoleString = "Administrateur";
                    break;
                default:
                    RoleString = "Sans rôle";
                    break;

            }
            Role = role;
            Annee = annee;
        }
    }
}
