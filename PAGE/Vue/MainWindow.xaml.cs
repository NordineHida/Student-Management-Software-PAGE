using PAGE.Model;
using PAGE.Vue.Ecran;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PAGE.Vue
{
    /// <summary>
    /// Fenêtre de bienvenue 
    /// </summary>
    /// <author>Nordine</author>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constucteur de la fenêtre (initialise les parametres s'ils existent
        /// </summary>
        /// <author>Nordine</author>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            //Deserialiser les parametres s'ils existent
            JsonSerializerParametre jsonParam = new JsonSerializerParametre();
            jsonParam.Load();


        }

        /// <summary>
        /// Affiche la fenêtre de bienvenue quelque seconde puis renvoi vers la fenetre de promo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // On affiche quelques secondes la fenêtre
            await Task.Delay(4000);

            // Fondu de fermeture
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            animation.Completed += (s, args) =>
            {
                // A la fin de l'animation, masquer la fenêtre
                this.Visibility = Visibility.Collapsed;
            };

            this.BeginAnimation(OpacityProperty, animation);

            // On attend la fin de l'animation
            await Task.Delay(1000);

            // On créer et on affiche la fenêtre pour choisir l'année et la promo
            ChoixPromo fenetrePromo = new ChoixPromo();
            fenetrePromo.Show();

            // On ferme la fenêtre 
            this.Close();
        }
    }
}
