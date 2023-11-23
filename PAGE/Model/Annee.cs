using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Model
{
    public class Annee
    {
        private int anneeDebut;
        private Promotion premiereAnnee;
        private Promotion deuxiemeAnnee;
        private Promotion troisiemeAnnee;

        public int AnneeDebut { get { return anneeDebut; } set {  anneeDebut = value; } }
        public Promotion PremiereAnnee { get {  return premiereAnnee; } set {  premiereAnnee = value; } }
        public Promotion DeuxiemeAnnee { get { return deuxiemeAnnee; } set { deuxiemeAnnee = value; } }
        public Promotion TroisiemeAnnee { get { return troisiemeAnnee; } set { troisiemeAnnee = value; } }

        public Annee() 
        {
        }  
    }
}
