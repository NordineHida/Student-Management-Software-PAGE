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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAGE.Vue.Ressources
{
    /// <summary>
    /// Logique d'interaction pour ParametresPage.xaml
    /// </summary>
    public partial class ParametresPage : Page
    {
        public event EventHandler ReturnToMainWindow;

        public ParametresPage()
        {
            InitializeComponent();
        }

        private void CloseParamWindow(object sender, RoutedEventArgs e)
        {
            ReturnToMainWindow?.Invoke(this,e);
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Récupérez la valeur du Slider
            double brightnessValue = BrightnessSlider.Value;

            // Utilisez la valeur pour ajuster la luminosité de l'application
            AdjustBrightness(brightnessValue);
        }

        private void AdjustBrightness(double brightnessValue)
        {
            byte brightness = (byte)(255 * brightnessValue);
            Color newColor = Color.FromRgb(brightness, brightness, brightness);
            this.Background = new SolidColorBrush(newColor);
        }
    }
}
