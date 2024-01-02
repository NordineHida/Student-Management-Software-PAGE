using PAGE.Model.PatternObserveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    public class Utilisateurs : Observable
    {
        private List<Utilisateur> listUser;
        /// <summary>
        /// liste des utilisateurs de l'application
        /// </summary>
        public List<Utilisateur> ListUser
        {
            get { return listUser; }
            set { listUser = value; }
        }

        /// <summary>
        /// Construit une liste d'utilisateur à partir d'une liste donnée en parametre
        /// </summary>
        /// <param name="listUser"></param>
        public Utilisateurs(List<Utilisateur> listUser)
        {
            this.listUser = listUser;
        }

        /// <summary>
        /// Ajoute un utilisateur à la liste
        /// </summary>
        /// <param name="user">utilisateur à ajouter</param>
        public void AddUser(Utilisateur user)
        {
            listUser.Add(user);
            Notifier("Utilisateur créé");
        }

        /// <summary>
        /// Supprime un utilisateur de la liste
        /// </summary>
        /// <param name="user">utilisateur à supprimer</param>
        public void DeleteUser(Utilisateur user)
        {
            listUser.Remove(user);
            Notifier("Utilisateur supprimé");
        }
    }
}
