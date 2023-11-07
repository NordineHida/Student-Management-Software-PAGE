using Microsoft.AspNetCore.Mvc;


namespace TestPAGE
{
    public class TestsExel
    {
        [Fact]
        public void Testnumapotoint()
        {
            Etudiant etudiantParser = new Etudiant(); 

            string apogeeString = "12345";
            int expectedApogeeInt = 12345;
            int actualApogeeInt = etudiantParser.ParseApogee(apogeeString);

            Assert.AreEqual(expectedApogeeInt, actualApogeeInt);
        }
    }
}