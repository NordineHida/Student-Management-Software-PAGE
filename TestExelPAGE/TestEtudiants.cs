using PAGE.Model;
using PAGE.Vue.Ecran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPAGE
{
    /// <summary>
    /// Classe permettant de réaliser des tests sur les étudiants
    /// </summary>
    public class TestEtudiants
    {
        /// <summary>
        /// methode testant la création d'un étudiant 
        /// </summary>
        /// <author>Lucas</author>
        [Fact]
        public void TestCreeretudiant()
        {
            int numApogee = 123;
            string nom = "Doe";
            string prenom = "John";
            SEXE sexe = SEXE.MASCULIN;
            string typeBac = "TypeBac1";
            string mail = "john.doe@example.com";
            GROUPE groupe = GROUPE.A1;
            bool estBoursier = true;
            REGIME typeFormation = REGIME.FA;
            DateTime dateNaissance = new DateTime(1990, 1, 1);
            string login = "johndoe";
            long telFixe = 1234567890;
            long telPortable = 9876543210;
            string adresse = "123 Main St";

            Etudiant etudiant = new Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, typeFormation,
                dateNaissance, login, telFixe, telPortable, adresse);

            Assert.Equal(numApogee, etudiant.NumApogee);
            Assert.Equal(nom, etudiant.Nom);
            Assert.Equal(prenom, etudiant.Prenom);
            Assert.Equal(sexe, etudiant.Sexe);
            Assert.Equal(typeBac, etudiant.TypeBac);
            Assert.Equal(mail, etudiant.Mail);
            Assert.Equal(groupe, etudiant.Groupe);
            Assert.Equal(estBoursier, etudiant.EstBoursier);
            Assert.Equal(typeFormation, etudiant.TypeFormation);
            Assert.Equal(dateNaissance, etudiant.DateNaissance);
            Assert.Equal(login, etudiant.Login);
            Assert.Equal(telFixe, etudiant.TelFixe);
            Assert.Equal(telPortable, etudiant.TelPortable);
            Assert.Equal(adresse, etudiant.Adresse);
        }

        /// <summary>
        /// Methode testant l'équivalence entre 2 étudiants 
        /// </summary>
        /// <author>Lucas</author>
        [Fact]

        public void TestEqualEtu()
        {
            Etudiant etudiant1 = new Etudiant(123, "Doe", "John", SEXE.MASCULIN, "TypeBac1", "john.doe@example.com",
                                              GROUPE.A1, true, REGIME.FA, new DateTime(1990, 1, 1),
                                              "johndoe", 1234567890, 9876543210, "123 Main St");

            Etudiant etudiant2 = new Etudiant(123, "Doe", "John", SEXE.MASCULIN, "TypeBac1", "john.doe@example.com",
                                              GROUPE.A1, true, REGIME.FA, new DateTime(1990, 1, 1),
                                              "johndoe", 1234567890, 9876543210, "123 Main St");

            Etudiant etudiant3 = new Etudiant(456, "Smith", "Alice", SEXE.FEMININ, "TypeBac2", "alice.smith@example.com",
                                              GROUPE.B1, false, REGIME.FC, new DateTime(1995, 5, 5),
                                              "alicesmith", 987654321, 123456789, "456 Oak St");

            Assert.True(etudiant1.Equals(etudiant2), "Les deux Etudiants identiques devraient être considérés comme égaux.");
            Assert.False(etudiant1.Equals(etudiant3), "Les deux Etudiants différents ne devraient pas être considérés comme égaux.");
        }

    }
}
