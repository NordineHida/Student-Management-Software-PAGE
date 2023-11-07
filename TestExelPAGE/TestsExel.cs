using PAGE.Model;


namespace TestExelPAGE
{
    public class TestsExel
    {

        /// <summary>
        /// M�thode de test pour v�rifier la convertion de string en int pour le num�ro Apog�e d'un �tudiant
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
        /// M�thode de test pour v�rifier la convertion de string en long pour le num�ro de portable d'un �tudiant
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
        /// M�thode de test pour v�rifier la convertion de string en long pour le num�ro de t�l�phone fixe d'un �tudiant
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
    }
    
}