using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    /// <summary>
    /// (De)Serialiseur de parametre
    /// </summary>
    /// <author>Nordine</author>
    public class JsonSerializerParametre
    {
        //Nom du fichier des paramètres
        private string file;

        public JsonSerializerParametre()
        {
            this.file = "settings.json";
        }
        
        /// <summary>
        /// Charge les parametre s'il existe
        /// </summary>
        /// <author>Nordine</author>
        public void Load()
        {
            //si les parametres existe on les deserialise
            if (File.Exists(this.file))
            {
                FileStream flux = new FileStream(file, FileMode.Open);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Parametre));
                Parametre tempParametre = ser.ReadObject(flux) as Parametre;
                
                //On applique les parametres deserialise au parametre de l'app
                Parametre.Instance.Langue = tempParametre.Langue;
                Parametre.Instance.PathGenerationWord = tempParametre.PathGenerationWord;
                
                flux.Close();
            }          
            //sinon on initialise les parametres et on les sauvegarde
            else
            {
                InitialiserParametre();
                Save();
            }

        }
        
        /// <summary>
        /// Initialise les parametres avec les valeurs par défaut
        /// </summary>
        /// <author>Nordine</author>
        private void InitialiserParametre()
        {
            Parametre.Instance.Langue = LANGUE.FRANCAIS;
            Parametre.Instance.PathGenerationWord = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        /// <summary>
        /// Sérialise les paramètres
        /// </summary>
        /// <author>Nordine</author>
        public void Save()
        {
            FileStream flux = new FileStream(file, FileMode.Create);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Parametre));
            ser.WriteObject(flux, Parametre.Instance);
            flux.Close();
        }
    }
}
