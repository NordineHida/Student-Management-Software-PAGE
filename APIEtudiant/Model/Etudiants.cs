using PAGE.Model.PatternObserveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEtudiant.Model
{
    /// <summary>
    /// Classe répertoriant tous les Etudiants
    /// </summary>
    /// <author>Laszlo</author>
    public class Etudiants : Observable
    {
        private List<Etudiant> listeEtu;

        /// <summary>
        /// Propriété notifiant quand on set sa valeur
        /// </summary>
        /// <author>Laszlo</author>
        public List<Etudiant> ListeEtu
        {
            get
            {
                return listeEtu;
            }
            set
            {
                listeEtu = value;
                Notifier("liste modifiee");
            }
        }

        /// <summary>
        /// Construit une liste Etudiants
        /// </summary>
        /// <author>Laszlo</author>
        public Etudiants(List<Etudiant> listeEtu)
        {
            this.listeEtu = listeEtu;
        }

        /// <summary>
        /// Ajoute un étudiant à la liste
        /// </summary>
        /// <param name="note">etudiant à ajouter</param>
        /// <author>Laszlo</author>
        public void AddEtu(Etudiant etu)
        {
            listeEtu.Add(etu);
            Notifier("etudiant ajoute");
        }



        /// <summary>
        /// Modifie un étudiant de la liste
        /// </summary>
        /// <param name="etu">etudiant à modifier</param>
        /// <author>Laszlo Nordine</author>
        public void UpdateEtu(Etudiant etu)
        {
            for (int i = 0; i < listeEtu.Count; i++)
            {
                if (listeEtu[i].NumApogee == etu.NumApogee)
                    listeEtu[i] = etu;
            }

            Notifier("etudiant modifie");
        }
    }
}
