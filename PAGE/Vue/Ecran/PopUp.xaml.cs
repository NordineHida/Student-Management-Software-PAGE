using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour PopUp.xaml
    /// </summary>
    /// 
    public partial class PopUp : Window
    {
        private string titre;
        private string message;
        private TYPEICON typeIcon;
        private BitmapImage picto;

        /// <summary>
        /// Constructeur de la fenetre PopUp
        /// </summary>
        /// <param name="titre">Titre du pop up</param>
        /// <param name="message">message du pop up</param>
        /// <param name="typeInfo">type d'info (erreur ?, succès ?, ...) pour adapter l'image</param>
        /// <author>Yamato</author>
        public PopUp(string titre, string message, TYPEICON typeIcon)
        {
            InitializeComponent();
            this.titre = titre;
            this.message = message;
            this.typeIcon = typeIcon;
            ChargerInfosPopUp();
        }

        /// <summary>
        /// Charge les informations de la pop up
        /// </summary>
        /// <author>Yamato</author>
        public void ChargerInfosPopUp()
        {
            this.Title = titre;
            txtMessage.Text = message;
            switch (typeIcon)
            {
                case TYPEICON.ERREUR:
                    picto = new BitmapImage(new Uri("pack://application:,,,/Vue/Ressources/Picto/error.png"));
                    break;
                case TYPEICON.SUCCES:
                    picto = new BitmapImage(new Uri("pack://application:,,,/Vue/Ressources/Picto/checkmark.png"));
                    break;
                case TYPEICON.INFORMATION:
                    picto = new BitmapImage(new Uri("pack://application:,,,/Vue/Ressources/Picto/information.png"));
                    break;
            }
            imgPicto.Source = picto;
        }

        /// <summary>
        /// Ferme la fenetre lors du click sur Ok
        /// </summary>
        /// <author>Yamato</author>
        private void OkClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
