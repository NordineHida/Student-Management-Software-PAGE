using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PAGE.Controlleurs;
using PAGE.Model;

namespace TestAPI
{
    public class TestsAPI
    {
        [Fact]
        public void TestAddEtu()
        {
            Mock<EtuManager> etuManagerMock = new Mock<EtuManager>();
            etuManagerMock.Setup(manager => manager.AddEtu(It.IsAny<Etudiant>())).Returns(true);

            EtuControlleur controller = new EtuControlleur();

            var etudiantToAdd = new Etudiant(
                1,
                "Doe",
                "John",
                SEXE.MASCULIN,
                "Baccalauréat S",
                "john.doe@example.com",
                "A",
                true,
                "Informatique",
                new DateTime(2000, 1, 15),
                "johndoe",
                1234567890,
                9876543210,
                "123 Rue de l'École"
            );

            
            var result = controller.AddEtu(etudiantToAdd);

            Assert.IsType<ActionResult>(result);

            if (result is OkResult)
            {
                
            }
            else
            {
                Assert.True(false, "L'ajout n'a pas réussi.");
            }
        }

        [Fact]
        public void TestGetAllEtu()
        {
            Mock<EtuManager> etuManagerMock = new Mock<EtuManager>();
            etuManagerMock.Setup(manager => manager.GetAllEtu()).Returns(new List<Etudiant>
            {
                new Etudiant (
                         1,
                        "Doe",
                        "John",
                        SEXE.MASCULIN,
                        "Baccalauréat S",
                        "john.doe@example.com",
                        "A",
                        true,
                        "Informatique",
                        new DateTime(2000, 1, 15),
                        "johndoe",
                        1234567890,
                        9876543210,
                        "123 Rue de l'École"
                        ),
                new Etudiant (
                        2,
                        "Smith",
                        "Jane",
                        SEXE.FEMININ,
                        "Baccalauréat L",
                        "jane.smith@example.com",
                        "B",
                        false,
                        "Mathématiques",
                        new DateTime(2001, 5, 20),
                        "janesmith",
                        9876543210,
                        1234567890,
                        "456 Avenue des Sciences" )
            });

            EtuControlleur controller = new EtuControlleur();

            var result = controller.GetAllEtu();

            Assert.IsType<ActionResult<IEnumerable<Etudiant>>>(result);

            if (result is ActionResult<IEnumerable<Etudiant>> okResult)
            {
                var etudiants = okResult.Value;

                Assert.NotNull(etudiants);
                Assert.NotEmpty(etudiants);
            }
            else
            {
                Assert.True(false, "Le résultat n'est pas un ActionResult<IEnumerable<Etudiant>>");
            }
        }


        [Fact]
        public void TestAddSeveralEtu()
        {
            Mock<EtuManager> etuManagerMock = new Mock<EtuManager>();
            etuManagerMock.Setup(manager => manager.AddSeveralEtu(It.IsAny<IEnumerable<Etudiant>>())).Returns(true);

            EtuControlleur controller = new EtuControlleur();

            var etudiantsToAdd = new List<Etudiant>
            {
                new Etudiant (
                    1,
                    "Doe",
                    "John",
                    SEXE.MASCULIN,
                    "Baccalauréat S",
                    "john.doe@example.com",
                    "A",
                    true,
                    "Informatique",
                    new DateTime(2000, 1, 15),
                    "johndoe",
                    1234567890,
                    9876543210,
                    "123 Rue de l'École"
                    ),
                new Etudiant (
                    2,
                    "Smith",
                    "Jane",
                    SEXE.FEMININ,
                    "Baccalauréat L",
                    "jane.smith@example.com",
                    "B",
                    false,
                    "Mathématiques",
                    new DateTime(2001, 5, 20),
                    "janesmith",
                    9876543210,
                    1234567890,
                    "456 Avenue des Sciences"
                    )
            };

            var result = controller.AddSeveralEtu(etudiantsToAdd);

            Assert.IsType<ActionResult>(result);

            if (result is OkResult)
            {
                
                var addedEtudiants = etuManagerMock.Object.GetAllEtu(); 
                Assert.NotNull(addedEtudiants);
                Assert.Equal(etudiantsToAdd.Count, addedEtudiants.Count());
            }
            else
            {
                Assert.True(false, "L'ajout de plusieurs étudiants n'a pas réussi.");
            }
        }

    }
}