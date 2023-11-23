using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAGE.Model
{
    /// <summary>
    /// Classe gérant les utilisateurs pouvant se connecter sur l'application et récupérer leurs droits
    /// </summary>
    public class Utilisateur
    {
        private string login;
        private string mdp;
        private string hashMdp;
        private Dictionary<int, ROLE> roles;

        /// <summary>
        /// Renvoie ou définit le login de l'utilisateur
        /// </summary>
        /// <author>Laszlo</author>
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        [JsonIgnore]
        /// <summary>
        /// Renvoie ou définit le login de l'utilisateur
        /// </summary>
        /// <author>Laszlo</author>
        public string Mdp
        {
            get { return mdp; }
            set { mdp = value; }
        }

        /// <summary>
        /// Renvoie le mot de passe hashé
        /// </summary>
        /// <author>Laszlo</author>
        public string HashMdp
        {
            get { return GetHashMdp(mdp); }
            set { hashMdp = value; }
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

        /// <summary>
        /// Hashe le mot de passe donné
        /// </summary>
        /// <param name="mdp">mot de passe à hasher</param>
        /// <returns></returns>
        public string GetHashMdp(string mdp)
        {
            string mdpHashed = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(mdp));
                foreach (byte b in hashValue)
                {
                    mdpHashed += $"{b:X2}";
                }
            }
            return mdpHashed;
        }

    }
}
