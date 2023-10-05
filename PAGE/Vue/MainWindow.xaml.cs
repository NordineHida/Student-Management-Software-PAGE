using PAGE.Model;
using System.Windows;
using Microsoft.Win32; // Pour OpenFileDialog

namespace PAGE.Vue
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Utilisez OpenFileDialog pour permettre à l'utilisateur de sélectionner un fichier
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers Excel (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtenez le chemin du fichier sélectionné
                string selectedFilePath = openFileDialog.FileName;

                // Appelez la méthode GetEtudiants avec le chemin du fichier
                LecteurExcel lc = new LecteurExcel();
                lc.GetEtudiants(selectedFilePath);
            }
        }
    }
}
