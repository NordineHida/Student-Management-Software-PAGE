using PAGE.Model;


namespace TestPAGE
{
    public class TestsExel
    {

        /// <summary>
        /// Méthode de test pour vérifier la convertion de string en int pour le numéro Apogée d'un étudiant
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]
        public void TestNumApogee()
        {
            string apogeeString = "12345";
            int expectedApogeeInt = 12345;
            int actualApogeeInt = int.Parse(apogeeString);

            Assert.Equal(expectedApogeeInt, actualApogeeInt);
        }


        /// <summary>
        /// Méthode de test pour vérifier la convertion de string en long pour le numéro de portable d'un étudiant
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]

        public void TestTelPortabke() 
        {
            string portableString = "0000000000";
            long expectedportableLong = 0000000000;
            long actualPortableLong = long.Parse(portableString);

            Assert.Equal(expectedportableLong, actualPortableLong);
        }


        /// <summary>
        /// Méthode de test pour vérifier la convertion de string en long pour le numéro de téléphone fixe d'un étudiant
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]

        public void TestTelFixe()
        {
            string fixeString = "0000000000";
            long expectedfixeLong = 0000000000;
            long actualfixeLong = long.Parse(fixeString);

            Assert.Equal(expectedfixeLong, actualfixeLong);
        }


        /// <summary>
        /// Méthode de test pour vérifier la convertion de string en SEXE pour le sexe de l'étudiant
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]

        public void TestSexe()
        {
            SEXE sexeEtu = SEXE.AUTRE;

            string sexe = "F";

            switch (sexe)
            {
                case "F":
                    sexeEtu = SEXE.FEMININ;
                    break;
                case "M":
                    sexeEtu = SEXE.MASCULIN;
                    break;
            }
            SEXE sexeexpected = SEXE.FEMININ;
            Assert.Equal(sexeexpected, sexeEtu);
        }



        /// <summary>
        /// Méthode de test pour vérifier la convertion de string en bool pour le status boursier ou non de l'étudiant
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]

        public void TestBoursier()
        {
            bool actualboursier = false;
            bool expectedboursier = true;

            string boursierstring = "OUI";

            if (boursierstring == "OUI") 
                actualboursier = true;

            Assert.Equal(expectedboursier, actualboursier);
        }
    }
    
}