using APIEtudiant.Model.Enumerations;

namespace APIEtudiant.Model
{

    // Classe gérant les utilisateurs pouvant se connecter sur l'application et récupérer leurs droits
    public class Utilisateur
    {
        private string login;
        private string mdp;
        private Dictionary<int, ROLE> roles;

        /// <summary>
        /// Renvoie ou définit le login de l'utilisateur
        /// </summary>
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        /// <summary>
        /// Renvoie ou définit le mot de passe de l'utilisateur
        /// </summary>
        public string Mdp
        {
            get { return mdp; }
            set { mdp = value; }
        }

        /// <summary>
        /// Construit un utilisateur en lui donnant un login et un mot de passe
        /// </summary>
        /// <param name="login">login de l'utilisateur</param>
        /// <param name="mdp">mot de passe de l'utilisateur</param>
        /// <author>Laszlo</author>
        public Utilisateur(string login, string mdp)
        {
            this.login = login;
            this.mdp = mdp;
            roles = new Dictionary<int, ROLE>();
        }

        /// <summary>
        /// Attribue un role pour une certaine année à l'utilisateur
        /// </summary>
        /// <param name="annee">annee pour laquelle le role est valide</param>
        /// <param name="role">role attribué</param>
        /// <author>Laszlo</author>
        public void AddRole(int annee, ROLE role)
        {
            roles.Add(annee, role);
        }


    }
}
