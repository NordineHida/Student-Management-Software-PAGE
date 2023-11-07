using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PAGE.Model
{
    /// <summary>
    /// Logique de paramètre
    /// </summary>
    /// <author>Nordine</author>
    public class Parametre
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

        private Parametre()
        {
            //si un fichier json existe je le prend, sinon param par defaut
            //ICI REMPALCER PAR JSON
            if (false == true )
            {

            }
            //Sinon parametre par defaut (langue :Français; path = bureau)
            else
            {
                this.Langue = LANGUE.FRANCAIS;
                this.pathGenerationWord = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

        }

        #endregion

        private string pathGenerationWord;

        /// <summary>
        /// Path de génération du word
        /// </summary>
        /// <author>Nordine</author>
        public string PathGenerationWord { get { return pathGenerationWord; } set { pathGenerationWord = value; } }

        private LANGUE langue;

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
                ChangerLangue(value);
            }
        }



        /// <summary>
        /// Change le dictionnaire de ressources utilisé pour changer la langue
        /// </summary>
        /// <param name="langue">nouvelle langue de l'app</param>
        /// <author>Nordine</author>
        public void ChangerLangue(LANGUE langue)
        {
            ResourceDictionary dictionnaire = new ResourceDictionary();

            switch (langue)
            {
                case LANGUE.ANGLAIS:
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case LANGUE.FRANCAIS:
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
                default:
                    dictionnaire.Source = new Uri("Vue\\Ressources\\Res\\StringResources.fr.xaml", UriKind.Relative);
                    break;
            }

            App.Current.Resources.MergedDictionaries.Add(dictionnaire);

        }
    }
}
