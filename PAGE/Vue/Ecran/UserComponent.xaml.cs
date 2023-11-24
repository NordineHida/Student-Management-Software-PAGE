using PAGE.Model;
using System.Windows.Controls;


namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour UserComponent.xaml
    /// </summary>
    public partial class UserComponent : UserControl
    {
        public Utilisateur Utilisateur { get; set; }

        public string Login { get; set; }
        public UserComponent(Utilisateur user)
        {
            InitializeComponent();
            DataContext = this;

            this.Utilisateur = user;
            this.Login = user.Login;

        }
    }
}
