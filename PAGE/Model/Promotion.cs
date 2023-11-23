using PAGE.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    public class Promotion
    {
        private Etudiants etudiants;
        private NOMPROMOTION nomPromotion;

        public Etudiants Etudiants { get {  return etudiants; } set {  etudiants = value; } }
        public NOMPROMOTION NomPromotion { get { return nomPromotion; } set {  nomPromotion = value; } }

        public Promotion(Etudiants etu, NOMPROMOTION nomPromo) 
        {
            this.etudiants = etu;
            this.nomPromotion = nomPromo;
        }
    }
}
