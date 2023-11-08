using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model.PatternObserveur
{
    /// <summary>
    /// Interface d'observateur
    /// </summary>
    /// <author></author>
    public interface IObservateur
    {
        /// <summary>
        /// Notifie que quelque chose a changer
        /// </summary>
        /// <param name="Message">changement</param>
        /// <author>Nordine</author>
        void Notifier(string Message);
    }
}
