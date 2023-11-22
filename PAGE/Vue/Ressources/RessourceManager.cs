using APIEtudiant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PAGE.Vue.Ressources
{
    /// <summary>
    /// Gère les ressources de l'applications
    /// </summary>
    /// <author>Nordine</author>
    public class RessourceManager
    {
        #region Singleton

        private static RessourceManager instance;

        /// <summary>
        /// Renvoie la seule instance de RessourceManager
        /// </summary>
        /// <author>Nordine</author>
        public static RessourceManager Instance
        {
            get
            {
                if (instance == null) instance = new RessourceManager();
                return instance;
            }
        }

        private RessourceManager()
        {
            // Initialisation des images
            Images = new Dictionary<string, BitmapImage>
            {
                { CATEGORIE.PERSONNEL.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/notePersonnel.png")) },
                { CATEGORIE.AUTRE.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/noteAutre.png")) },
                { CATEGORIE.ABSENTEISME.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/noteAbsenteisme.png")) },
                { CATEGORIE.MEDICAL.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/noteMedical.png")) },
                { CATEGORIE.ORIENTATION.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/noteOrientation.png")) },
                { CATEGORIE.RESULTATS.ToString(), new BitmapImage(new Uri("pack://application:,,,/PAGE;component/Vue/Ressources/Img/Notes/noteResultats.png")) }
            };
        }
        #endregion

        private Dictionary<string, BitmapImage> Images;

        /// <summary>
        /// renvoi l'image associé au nom
        /// </summary>
        /// <param name="nom">nom de l'image</param>
        /// <returns>image associé</returns>
        /// <author>Nordine</author>
        public BitmapImage GetImage(string nom)
        {
            return Instance.Images[nom];
        }
    }
}
