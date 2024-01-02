using PAGE.Model.Enumerations;
using PAGE.Model.PatternObserveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE.Model
{
    [DataContract]
    /// <summary>
    /// Logique de paramètre
    /// </summary>
    /// <author>Nordine</author>
    public class Parametre : Observable
    {

        #region Singleton

        private static Parametre instance;

        /// <summary>
        /// Renvoi la seule instance de Parametre
        /// </summary>
        /// <author>Nordine</author>
        public static Parametre Instance
        {
            get
            {
                if (instance == null) instance = new Parametre();
                return instance;
            }
        }

        private Parametre() : base() {}

        #endregion

        private string pathGenerationWord;

        [DataMember]
        /// <summary>
        /// Path de génération du word
        /// </summary>
        /// <author>Nordine</author>
        public string PathGenerationWord { get { return pathGenerationWord; } set { pathGenerationWord = value; } }

        private LANGUE langue;

        [DataMember]
        /// <summary>
        /// Langue des paramètres
        /// </summary>
        /// <author>Nordine</author>
        public LANGUE Langue
        {
            get { return this.langue; }

            set 
            { 
                this.langue = value;
                this.Notifier(value.ToString());
            }
        }
    }
}
