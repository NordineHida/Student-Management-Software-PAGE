using APIEtudiant.Controllers;
using APIEtudiant.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestAPI
{
    public class TestsAPI
    {
        /// <summary>
        /// M�thode de test pour AddEtu 
        /// On v�rifie si il est possible d'ajouter un etudiant fictif 
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
        [Fact]
        public void TestAddEtu()
        {
            Mock<EtuManager> etuManagerMock = new Mock<EtuManager>();
            etuManagerMock.Setup(manager => manager.AddEtu(It.IsAny<Etudiant>())).Returns(true);

            EtuControlleur controller = new EtuControlleur();

            Etudiant etudiantToAdd = new Etudiant(
                1,
                "Doe",
                "John",
                SEXE.MASCULIN,
                "Baccalaur�at S",
                "john.doe@example.com",
                "A",
                true,
                "Informatique",
                new DateTime(2000, 1, 15),
                "johndoe",
                1234567890,
                9876543210,
                "123 Rue de l'�cole"
            );

            
            ActionResult result = controller.AddEtu(etudiantToAdd);

            Assert.IsType<ActionResult>(result);

            if (result is OkResult)
            {
                Assert.True(true, "L'ajout est un succ�s.");
            }
            else
            {
                Assert.True(false, "L'ajout n'a pas r�ussi.");
            }
        }

        /// <summary>
        /// M�thode de test pour GetAllEtu 
        /// On v�rifie si il est possible de r�cup�rer tout les �tudiants dans la liste
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
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
                        "Baccalaur�at S",
                        "john.doe@example.com",
                        "A",
                        true,
                        "Informatique",
                        new DateTime(2000, 1, 15),
                        "johndoe",
                        1234567890,
                        9876543210,
                        "123 Rue de l'�cole"
                        ),
                new Etudiant (
                        2,
                        "Smith",
                        "Jane",
                        SEXE.FEMININ,
                        "Baccalaur�at L",
                        "jane.smith@example.com",
                        "B",
                        false,
                        "Math�matiques",
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
                Assert.True(false, "Le r�sultat n'est pas un ActionResult<IEnumerable<Etudiant>>");
            }
        }


        /// <summary>
        /// M�thode de test pour AddSeveralEtu 
        /// On v�rifie si il est possible d'ajouter plusieurs etudiant fictif 
        /// </summary>
        /// <Author>Lucas PRUNER</Author>
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
                    "Baccalaur�at S",
                    "john.doe@example.com",
                    "A",
                    true,
                    "Informatique",
                    new DateTime(2000, 1, 15),
                    "johndoe",
                    1234567890,
                    9876543210,
                    "123 Rue de l'�cole"
                    ),
                new Etudiant (
                    2,
                    "Smith",
                    "Jane",
                    SEXE.FEMININ,
                    "Baccalaur�at L",
                    "jane.smith@example.com",
                    "B",
                    false,
                    "Math�matiques",
                    new DateTime(2001, 5, 20),
                    "janesmith",
                    9876543210,
                    1234567890,
                    "456 Avenue des Sciences"
                    )
            };

            ActionResult result = controller.AddSeveralEtu(etudiantsToAdd);

            Assert.IsType<ActionResult>(result);

            if (result is OkResult)
            {
                
                var addedEtudiants = etuManagerMock.Object.GetAllEtu(); 
                Assert.NotNull(addedEtudiants);
                Assert.Equal(etudiantsToAdd.Count, addedEtudiants.Count());
            }
            else
            {
                Assert.True(false, "L'ajout de plusieurs �tudiants n'a pas r�ussi.");
            }
        }

    }
}