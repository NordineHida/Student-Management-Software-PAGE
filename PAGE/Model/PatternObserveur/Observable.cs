using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model.PatternObserveur
{
    //Serialisable pour que les parametres puisse être sérialisé (mais on ne sérialise rien ici)
    [DataContract]
    /// <summary>
    /// Classe abstraite d'observable
    /// </summary>
    /// <author>Nordine</author>
    public abstract class Observable
    {

        private List<IObservateur> observateurs;

        /// <summary>
        /// Constructeur (initialiser la liste des observateurs)
        /// </summary>
        /// <author>Nordine</author>
        public Observable()
        {
            this.observateurs = new List<IObservateur>();
        }

        /// <summary>
        /// Ajout l'observateur à la liste des observateur
        /// </summary>
        /// <param name="observateur">observateur a ajoutr</param>
        /// <author>Nordine</author>
        public void Register(IObservateur observateur)
        {
            this.observateurs.Add(observateur);
        }

        /// <summary>
        /// Supprime l'observateur de la liste des observateur
        /// </summary>
        /// <param name="observateur">l'observateur à supprimer</param>
        /// <author>Nordine</author>
        public void UnRegister(IObservateur observateur)
        {
            this.observateurs.Remove(observateur);
        }

        /// <summary>
        /// Notifie tout les observateur de la modification
        /// </summary>
        /// <param name="Message">message de modification</param>
        /// <author>Nordine</author>
        public void Notifier(string Message)
        {
            //On verifie si la liste n'est pas null car la désérialisation ne passe pas par le constructeur qui est censé initialiser cette liste
            if(this.observateurs != null)
            {
                foreach (IObservateur obs in this.observateurs)
                {
                    obs.Notifier(Message);
                }
            }
        }
    }
}
